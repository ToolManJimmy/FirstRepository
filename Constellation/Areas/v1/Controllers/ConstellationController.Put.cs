using Lion.ExAPI.Constellation.Model.Domain;
using Lion.ExAPI.Constellation.Utility;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace Constellation.Areas.v1.Controllers
{
    public class PutConstellationController : ApiController
    {
        public IHttpActionResult Put()
        {
            var httpRequest = HttpContext.Current.Request.InputStream;
            var constellationList = ProjectFun.GetJsonToList(httpRequest);

            ConstellationDomain domain = new ConstellationDomain();

            foreach (var constellation in constellationList)
            {
                Regex regexEn = new Regex(@"[a-zA-Z]+$");
                Regex regexCh = new Regex(@"[\u4e00-\u9fa5]");

                if (!regexEn.IsMatch(constellation.Name))
                {
                    return Ok("Name輸入錯誤");
                }
                if (!regexCh.IsMatch(constellation.Constellation))
                {
                    return Ok("Constellation輸入錯誤");
                }
                domain.PutConstellation(constellation);
            }

            return Ok();
        }
    }
}