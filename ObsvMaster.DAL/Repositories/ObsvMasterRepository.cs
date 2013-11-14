using NHibernate;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace ObsvMaster.DAL.Repositories
{
    public class ObsvMasterRepository
    {
        protected ISession _session;

        public ObsvMasterRepository(ISession session)
        {
            _session = session;
        }

        public List<MasterObsTrip> GetObsTrips(String vesselName, int? dateYear = 0, string port = "", string obsvCode = "", string obsvTripCode = "", string obsvProgCode = "", int? lastModifiedDateYear = 0, string lastModifiedBy = "", string statusCode = "",int pageSize = -1, int offset = 0, string sortedBy = "", string sortDir = "")
        {
            var query = _session.Query<MasterObsTrip>();
            query = query.Where(x => x.IsActive == true);
            if (!String.IsNullOrEmpty(vesselName))
                query = query.Where(x => x.Vessel.Name.Contains(vesselName));
            if (dateYear.HasValue && dateYear > 0)
                query = query.Where(x => x.StartDate.Year == dateYear || x.EndDate.Year == dateYear);
            if (!String.IsNullOrEmpty(port))
                //filter only on departure ports
                query = query.Where(x => x.StartPort.Name == port);
            if (!String.IsNullOrEmpty(obsvCode))
                query = query.Where(x => x.ObsvCode == obsvCode);
            if (!String.IsNullOrEmpty(obsvTripCode))
                query = query.Where(x => x.ObsvTripCode == obsvTripCode);
            if (!String.IsNullOrEmpty(obsvProgCode))
                query = query.Where(x => x.ObsvProg.Code == obsvProgCode);
            if (!String.IsNullOrEmpty(lastModifiedBy))
                query = query.Where(x => x.LastModifiedBy == lastModifiedBy);
            if (lastModifiedDateYear.HasValue && lastModifiedDateYear > 0)
                query = query.Where(x => x.LastModifiedDate.Year == lastModifiedDateYear);
            if (!String.IsNullOrEmpty(statusCode))
                query = query.Where(x => x.Status.Code == statusCode);
            if (!String.IsNullOrEmpty(sortedBy))
            {
                switch (sortedBy)
                {
                    case "VesselName":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.Vessel.Name) : query.OrderByDescending(x => x.Vessel.Name);
                        break;
                    case "StartDate":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.StartDate) : query.OrderByDescending(x => x.StartDate);
                        break;
                    case "StartPortName":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.StartPort.Name) : query.OrderByDescending(x => x.StartPort.Name);
                        break;
                    case "EndDate":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.EndDate) : query.OrderByDescending(x => x.EndDate);
                        break;
                    case "EndPortName":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.EndPort.Name) : query.OrderByDescending(x => x.EndPort.Name);
                        break;
                    case "ObsvCode":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.ObsvCode) : query.OrderByDescending(x => x.ObsvCode);
                        break;
                    case "ObsvTripCode":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.ObsvTripCode) : query.OrderByDescending(x => x.ObsvTripCode);
                        break;
                    case "ObsvProgCode":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.ObsvProg.Code) : query.OrderByDescending(x => x.ObsvProg.Code);
                        break;
                    case "LastModifiedDate":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.LastModifiedDate) : query.OrderByDescending(x => x.LastModifiedDate);
                        break;
                    case "LastModifiedBy":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.LastModifiedBy) : query.OrderByDescending(x => x.LastModifiedBy);
                        break;
                    case "Status":
                        query = (sortDir.ToUpper() == "ASC") ? query.OrderBy(x => x.Status.Code) : query.OrderByDescending(x => x.Status.Code);
                        break;
                }
            }
            if (pageSize > 0)
                query = query.Skip(offset).Take(pageSize);
            return query.ToList();
        }
    }
}
