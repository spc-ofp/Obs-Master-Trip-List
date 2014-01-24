using LINQtoCSV;
using ObsvMaster.DAL.Repositories;
using ObsvMaster.Domain;
using ObsvMaster.Web.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public IEnumerable<MasterObsTripModel> Get(String vesselName = "", int dateYear = 0, string port = "", string obsvCode = "", string obsvTripCode = "", string obsvProgCode = "", int lastModifiedDateYear = 0, string lastModifiedBy = "", string statusCode = "", int pageSize = 50, int offset = 0, string sortedBy = "", string sortDir = "")
        {
            if (pageSize < 10)
                pageSize = 10;
            else
                if (pageSize > 300)
                    pageSize = 300;

            IEnumerable<MasterObsTrip> masterTripList = _obsvMasterRepo.GetObsTrips(vesselName, dateYear, port, obsvCode, obsvTripCode, obsvProgCode, lastModifiedDateYear, lastModifiedBy, statusCode, pageSize, offset, sortedBy, sortDir);
            IList<MasterObsTripModel> masterTripModelList = new List<MasterObsTripModel>();
            foreach (MasterObsTrip trip in masterTripList)
                masterTripModelList.Add(new MasterObsTripModel(trip));
            return masterTripModelList;
        }

        public HttpResponseMessage GetAllTrips(String vesselName = "", int? dateYear = 0, string port = "", string obsvCode = "", string obsvTripCode = "", string obsvProgCode = "", int? lastModifiedDateYear = 0, string lastModifiedBy = "", string statusCode = "")
        {
            IEnumerable<MasterObsTrip> masterTripList = _obsvMasterRepo.GetObsTrips(vesselName, dateYear, port, obsvCode, obsvTripCode, obsvProgCode, lastModifiedDateYear, lastModifiedBy, statusCode);
            IList<MasterObsTripModel> masterTripModelList = new List<MasterObsTripModel>();
            foreach (MasterObsTrip trip in masterTripList)
                masterTripModelList.Add(new MasterObsTripModel(trip));

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', // comma delimited
                FirstLineHasColumnNames = true, // no column names in first record
                FileCultureName = "fr-FR" // use formats used in The Netherlands
            };
            CsvContext cc = new CsvContext();
            cc.Write(masterTripModelList, writer, outputFileDescription);
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "ObserverTripList.csv";
            return response;
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
            return _repository.GetAll<Status>().Select(x => new { x.Code, x.Label });
        }

        public HttpResponseMessage GetCount(String vesselName = "", int? dateYear = 0, string port = "", string obsvCode = "", string obsvTripCode = "", string obsvProgCode = "", int? lastModifiedDateYear = 0, string lastModifiedBy = "", string statusCode = "")
        {
            return new HttpResponseMessage { Content = new StringContent("{\"count\":" + _obsvMasterRepo.GetObsTrips(vesselName, dateYear, port, obsvCode, obsvTripCode, obsvProgCode, lastModifiedDateYear, lastModifiedBy, statusCode).Count.ToString() + "}", System.Text.Encoding.UTF8, "application/json") };
        }


        public IEnumerable<Object> GetVesselNames()
        {
            return _repository.GetAll<Vessel>().Select(x => new { x.Name });
        }

        public IEnumerable<Object> GetVesselLookUp(string search = "")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Vessel>(x => x.Name.ToUpper().StartsWith(search)).Select(x => new { x.Name }).Take(10);
        }

        public IEnumerable<Object> GetObserverLookUp(string search = "")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Observer>(x => x.FamilyName.ToUpper().Contains(search) || x.FirstName.ToUpper().Contains(search) || x.Code.ToUpper().Contains(search)).Select(x => new { x.Code, x.FirstName, x.FamilyName }).Take(10);
        }

        public Object GetObserver(string code)
        {
            return _repository.Find<Observer>(x => x.Code == code).Select(x => new { x.Code, x.FirstName, x.FamilyName }).FirstOrDefault();
        }

        public IEnumerable<Object> GetPortNames()
        {
            return _repository.GetAll<Port>().Select(x => new { x.Name });
        }

        public IEnumerable<Object> GetPortLookUp(string search = "")
        {
            if (search.Length < 2)
                return null;
            search = search.ToUpper();
            return _repository.Find<Port>(x => x.Name.ToUpper().StartsWith(search)).Select(x => new { x.Name }).Take(10);
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
                trip.IsActive = true;
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

        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                MasterObsTrip trip = _repository.Get<MasterObsTrip>(id);
                trip.IsActive = false;
                trip.LastModifiedDate = DateTime.Now;
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

        private MasterObsTrip ModelToTrip(MasterObsTripModel model)
        {
            MasterObsTrip trip = new MasterObsTrip();
            trip.EndDate = model.EndDate;
            if (!String.IsNullOrEmpty(model.EndPortName))
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
            trip.IsActive = model.IsActive;
            return trip;
        }


    }
}