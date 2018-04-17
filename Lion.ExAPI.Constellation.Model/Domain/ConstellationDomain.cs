using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lion.ExAPI.Constellation.Model.Repository;
using Lion.ExAPI.Constellation.Model.ViewModel;
using Lion.ExAPI.Constellation.Model.DataModel;
using Dapper;
using LinqKit;

namespace Lion.ExAPI.Constellation.Model.Domain
{
    public class ConstellationDomain
    {
        private ConstellationRepository constellationRepository = new ConstellationRepository();

        public IEnumerable<ConstellationModel> ToViewModel(List<ConstellationData> list)
        {
            List<ConstellationModel> viewModel = new List<ConstellationModel>();
            if (list != null && list.Count > 0)
                viewModel = list.Select(x => new ConstellationModel
                {
                    Constellation = x.Constellation,
                    Name = x.Name
                }).ToList();

            return viewModel;
        }

        public List<ConstellationData> GetAllList()
        {
            return constellationRepository.GetConstellation();
        }

        public List<ConstellationData> GetConstellation(List<int> idList = null, List<string> nameList = null, List<string> constellationList = null, string mode = null)
        {
            var orConstellation = PredicateBuilder.New<ConstellationData>();
            var andConstellation = PredicateBuilder.New<ConstellationData>();

            List<ConstellationData> list = new List<ConstellationData>();
            if (idList != null && idList.Count() > 0)
            {
                orConstellation = orConstellation.Or(x => idList.Where(y => x.Id == y).Count() > 0);
                andConstellation = andConstellation.And(x => idList.Where(y => x.Id == y).Count() > 0);
            }
            if (nameList != null && nameList.Count() > 0)
            {
                orConstellation = orConstellation.Or(x => nameList.Where(y => x.Name == y).Count() > 0);
                andConstellation = andConstellation.And(x => nameList.Where(y => x.Name == y).Count() > 0);
            }
            if (constellationList != null && constellationList.Count() > 0)
            {
                orConstellation = orConstellation.Or(x => constellationList.Where(y => x.Constellation == y).Count() > 0);
                andConstellation = andConstellation.And(x => constellationList.Where(y => x.Constellation == y).Count() > 0);
            }
            if (mode == "and")
            {
                return constellationRepository.GetConstellation().Where(andConstellation).ToList();
            }
            return constellationRepository.GetConstellation().Where(orConstellation).ToList();
        }

        public void PostConstellation(ConstellationModel constellation)
        {
            constellationRepository.PostConstellation(constellation.Name, constellation.Constellation);
        }

        public void PutConstellation(ConstellationModel constellation)
        {
            constellationRepository.PutConstellation(constellation.Name, constellation.Constellation);
        }

        public void DeleteConstellation(List<string> name)
        {
            foreach (var item in name)
            {
                constellationRepository.DeleteConstellation(item);
            }
        }
    }
}