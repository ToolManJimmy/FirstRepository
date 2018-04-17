using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Lion.ExAPI.Constellation.Model.Domain;

namespace Constellation.Areas.v1.Controllers
{
    public class DeleteConstellationController : ApiController
    {
        public IHttpActionResult Delete(string name)
        {
            var constellationDomain = new ConstellationDomain();
            List<string> nameList = new List<string>();
            Regex regexEn = new Regex(@"[a-zA-Z]+$");
            if (name != null)
            {
                nameList = name.Split(',').OfType<string>().ToList();
                if (name.Count() > 0)
                {
                    foreach (var item in nameList)
                    {
                        if (!regexEn.IsMatch(item))
                        {
                            return Ok("Name輸入錯誤");
                        }
                    }
                }
                constellationDomain.DeleteConstellation(nameList);
                return Ok("刪除成功");
            }
            return NotFound();
        }
    }
}