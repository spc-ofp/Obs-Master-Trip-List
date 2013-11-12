using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObsvMaster.Web.Models
{
    public class MasterObsTripModel
    {
        public int Id { get; set; }
        public String VesselName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String StartPortName { get; set; }
        public String EndPortName { get; set; }
        public String ObsvCode { get; set; }
        public String ObsvTripCode { get; set; }
        public String ObsvProgCode { get; set; }
        public String Status { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public String LastModifiedBy { get; set; }

        public MasterObsTripModel(MasterObsTrip trip)
        {
            this.Id = trip.Id;
            if (trip.Vessel != null)
                this.VesselName = trip.Vessel.Name;
            this.StartDate = trip.StartDate;
            this.EndDate = trip.EndDate;
            if(trip.StartPort!=null)
                this.StartPortName = trip.StartPort.Name;
            if (trip.EndPort != null)
                this.EndPortName = trip.EndPort.Name;
            this.ObsvCode = trip.ObsvCode;
            if (trip.ObsvProg != null)
                this.ObsvProgCode = trip.ObsvProg.Code;
            this.ObsvTripCode = trip.ObsvTripCode;
            this.LastModifiedDate = trip.LastModifiedDate;
            if (trip.Status != null)
                this.Status = trip.Status.Code;
            this.LastModifiedBy = trip.LastModifiedBy;
        }

        public MasterObsTripModel(MasterObsTripHistory trip)
        {
            this.Id = trip.Id;
            if (trip.Vessel != null)
                this.VesselName = trip.Vessel.Name;
            this.StartDate = trip.StartDate;
            this.EndDate = trip.EndDate;
            if (trip.StartPort != null)
                this.StartPortName = trip.StartPort.Name;
            if (trip.EndPort != null)
                this.EndPortName = trip.EndPort.Name;
            this.ObsvCode = trip.ObsvCode;
            if (trip.ObsvProg != null)
                this.ObsvProgCode = trip.ObsvProg.Code;
            this.ObsvTripCode = trip.ObsvTripCode;
            this.LastModifiedDate = trip.LastModifiedDate;
            if (trip.Status != null)
                this.Status = trip.Status.Code;
            this.LastModifiedBy = trip.LastModifiedBy;
        }

        public MasterObsTripModel()
        { }
    }
}