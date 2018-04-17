using Lion.ExAPI.Constellation.Model.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Constellation.Areas.v1.Controllers
{
    public class GetConstellationController : ApiController
    {
        public IHttpActionResult Get(string id = null, string name = null, string constellation = null, string mode = null)
        {
            var constellationDomain = new ConstellationDomain();
            List<int> idList = new List<int>();
            List<string> nameList = new List<string>();
            List<string> constellationList = new List<string>();
            Regex regexEn = new Regex(@"[a-zA-Z]+$");
            Regex regexCh = new Regex(@"[\u4e00-\u9fa5]");

            if (id == null && name == null && constellation == null)
            {
                var query = constellationDomain.GetAllList();
                return Ok(query);
            }
            else
            {
                if (id != null)
                {
                    idList = id.Split(',').OfType<int>().ToList();
                }
                if (name != null)
                {
                    nameList = name.Split(',').OfType<string>().ToList();
                    foreach (var item in nameList)
                    {
                        if (!regexEn.IsMatch(item))
                        {
                            return Ok("Name輸入錯誤");
                        }
                    }
                }
                if (constellation != null)
                {
                    constellationList = constellation.Split(',').OfType<string>().ToList();
                    foreach (var item in constellationList)
                    {
                        if (!regexCh.IsMatch(item))
                        {
                            return Ok("Constellation輸入錯誤");
                        }
                    }
                }

                var list = constellationDomain.GetConstellation(idList, nameList, constellationList, mode);

                return Ok(list);
            }
        }
    }
}