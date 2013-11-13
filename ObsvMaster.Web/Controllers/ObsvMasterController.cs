using ObsvMaster.DAL.Repositories;
using ObsvMaster.Domain;
using ObsvMaster.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObsvMaster.Web.Controllers
{
    public class ObsvMasterController : ApiController
    {
        private readonly Repository _repository;
        private readonly ObsvMasterRepository _obsvMasterRepo;

        public ObsvMasterController()
        {
            _repository = new Repository(WebApiApplication.UnitOfWork.Session);
            _obsvMasterRepo = new ObsvMasterRepository(WebApiApplication.UnitOfWork.Session);
        }

        public IEnumerable<MasterObsTripModel> Get(String vesselName = "",int dateYear =0,string port="",string obsvCode="",string obsvTripCode="",string obsvProgCode="",int lastModifiedDateYear=0,string lastModifiedBy="",string statusCode="", int pageSize = 50, int offset = 0,string sortedBy="",string sortDir="")
        {
            if (pageSize < 10)
                pageSize = 10;
            else
                if (pageSize > 300)
                    pageSize = 300;

            IEnumerable<MasterObsTrip> masterTripList = _obsvMasterRepo.GetObsTrips(vesselName, dateYear, port, obsvCode,obsvTripCode,obsvProgCode,lastModifiedDateYear,lastModifiedBy,statusCode ,pageSize, offset, sortedBy, sortDir);
            IList<MasterObsTripModel> masterTripModelList = new List<MasterObsTripModel>();
            foreach (MasterObsTrip trip in masterTripList)
                masterTripModelList.Add(new MasterObsTripModel(trip));
            return masterTripModelList;
        }

        public IEnumerable<MasterObsTripModel> GetHistory(int id)
        {
            IEnumerable<MasterObsTripHistory> historyList = _repository.Find<MasterObsTripHistory>(x => x.Id == id).OrderByDescending(x => x.LastModifiedDate);
            IList<MasterObsTripModel> masterTripModelList = new List<MasterObsTripModel>();
            foreach (MasterObsTripHistory trip in historyList)
                masterTripModelList.Add(new MasterObsTripModel(trip));
            return masterTripModelList;
        }

        public IEnumerable<Object> GetAllStatus()
        {
            return _repository.GetAll<Status>().Select(x => new { x.Code,x.Label });
        }

        public HttpResponseMessage GetCount(String vesselName = "", int dateYear = 0, string port = "", string obsvCode = "", string obsvTripCode = "", string obsvProgCode = "", int lastModifiedDateYear = 0, string lastModifiedBy = "", string statusCode = "")
        {
            return new HttpResponseMessage { Content = new StringContent("{\"count\":" + _obsvMasterRepo.GetObsTrips(vesselName, dateYear, port, obsvCode, obsvTripCode, obsvProgCode, lastModifiedDateYear, lastModifiedBy, statusCode).Count.ToString() + "}", System.Text.Encoding.UTF8, "application/json") };
        }

        public IEnumerable<Object> GetVesselNames()
        {
            return _repository.GetAll<Vessel>().Select(x => new { x.Name });
        }

        public IEnumerable<Object> GetVesselLookUp(string search="")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Vessel>(x => x.Name.ToUpper().StartsWith(search)).Select(x => new { x.Name }).Take(10);
        }

        public IEnumerable<Object> GetPortNames()
        {
            return _repository.GetAll<Port>().Select(x => new { x.Name });
        }

        public IEnumerable<Object> GetPortLookUp(string search="")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Port>(x=>x.Name.ToUpper().StartsWith(search)).Select(x => new { x.Name }).Take(10);
        }

        public IEnumerable<Object> GetObsProgLookUp(string search = "")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Program>(x => x.Code.ToUpper().StartsWith(search)).Select(x => new { x.Code }).Take(10);
        }


        public MasterObsTripModel Get(int id)
        {
            MasterObsTrip trip = _repository.Get<MasterObsTrip>(id);
            return new MasterObsTripModel(trip);
        }

        public HttpResponseMessage Post([FromBody]MasterObsTripModel model)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                MasterObsTrip trip = ModelToTrip(model);
                _repository.Save<MasterObsTrip>(trip);
                WebApiApplication.UnitOfWork.Commit();
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Critical Post Exception"
                };
            }
            return message;
        }

        public HttpResponseMessage Put(int id, [FromBody]MasterObsTripModel model)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                MasterObsTrip trip = ModelToTrip(model);
                trip.Id = id;
                _repository.SaveOrUpdate<MasterObsTrip>(trip);
                WebApiApplication.UnitOfWork.Commit();
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Critical Post Exception"
                };
            }
            return message;
        }

        public void Delete(int id)
        {
        }

        private MasterObsTrip ModelToTrip(MasterObsTripModel model)
        {
            MasterObsTrip trip = new MasterObsTrip();
            trip.EndDate = model.EndDate;
            if(!String.IsNullOrEmpty(model.EndPortName))
                trip.EndPort = _repository.Find<Port>(x => x.Name == model.EndPortName).SingleOrDefault();
            trip.LastModifiedBy = model.LastModifiedBy;
            trip.LastModifiedDate = DateTime.Now;
            trip.ObsvCode = model.ObsvCode;
            //trip.ObsvProgCode = model.ObsvProgCode;
            if (!String.IsNullOrEmpty(model.ObsvProgCode))
                trip.ObsvProg = _repository.Find<Program>(x => x.Code == model.ObsvProgCode).SingleOrDefault();
            trip.ObsvTripCode = model.ObsvTripCode;
            trip.StartDate = model.StartDate;
            if (!String.IsNullOrEmpty(model.StartPortName))
                trip.StartPort = _repository.Find<Port>(x => x.Name == model.StartPortName).SingleOrDefault();
            if (!String.IsNullOrEmpty(model.VesselName))
                trip.Vessel = _repository.Find<Vessel>(x => x.Name == model.VesselName).SingleOrDefault();
            if (!String.IsNullOrEmpty(model.Status))
                trip.Status = _repository.Get<Status>(model.Status);
            return trip;
        }

        
    }
}