using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Services.Interfaces
{
    public interface IOwnerInfoService
    {
        OwnerInfo GetOwnerInfo(int ownerId);

        PropertiesInfo[] GetOwnerProperties(int ownerId);

        ServiceTypeInfo[] GetServiceTypes();

        CampaniesInfoResponse[] GetCompanies(CampaniesInfoRequest request, int ownerId);

        ReceiptInfoResponse[] GetReceipts(ReceiptsInfoRequest request, int ownerId);
    }
}
