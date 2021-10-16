using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class CityArea
    {

        public IEnumerable<tCity> getcity()
        {
            using (demodbEntities db = new demodbEntities())
            {
                var q = db.tCity.OrderBy(x => x.fID);
                return q.ToList();
            }
        }

        public IEnumerable<tArea> GetArea(int Cityid)
        {
            using (demodbEntities db = new demodbEntities())
            {
                var q = db.tArea.Where(x => x.fk_City == Cityid).OrderBy(x => x.fid);
                return q.ToList();
            }
        }
    }
}