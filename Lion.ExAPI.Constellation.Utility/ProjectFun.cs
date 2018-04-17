using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lion.ExAPI.Constellation.Model.ViewModel;
using Lion.ExAPI.Constellation.Model.Domain;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace Lion.ExAPI.Constellation.Utility
{
    public class ProjectFun
    {
        public static List<ConstellationModel> GetJsonToList(Stream httpRequest)
        {
            string strUrlToJson = string.Empty;
            List<ConstellationModel> constellationList = new List<ConstellationModel>();
            ConstellationDomain domain = new ConstellationDomain();

            if (httpRequest.CanRead)
            {
                var reader = new StreamReader(httpRequest);
                strUrlToJson = reader.ReadToEnd();

                constellationList = JsonConvert.DeserializeObject<List<ConstellationModel>>(strUrlToJson);
            }
            return constellationList;
        }

        public static string GetMatch(string matchEN = null, string matchInt = null, string matchCH = null, string msg = null)
        {
            Regex regexEn = new Regex(@"[a-zA-Z]+$");
            Regex regexInt = new Regex(@"[0-9]+$");
            Regex regexCh = new Regex(@"[\u4e00-\u9fa5]");
            if (matchEN != null)
            {
                if (!regexEn.IsMatch(matchEN))
                {
                    return msg + "需輸入英文";
                }
            }
            if (matchInt != null)
            {
                if (!regexInt.IsMatch(matchInt))
                {
                    return msg + "需輸入數字";
                }
            }
            if (matchCH != null)
            {
                if (!regexCh.IsMatch(matchCH))
                {
                    return msg + "需輸入數字";
                }
            }
            return null;
        }
    }
}