using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    public interface IMapper<TDomain, TDataModel>
    {
        TDomain ToDomain(TDataModel dataModel);
        IEnumerable<TDomain> ToDomain(IEnumerable<TDataModel> dataModels);
        TDataModel ToDataModel(TDomain domainEntity);
        IEnumerable<TDataModel> ToDataModel(IEnumerable<TDomain> dataModels);
    }
}
