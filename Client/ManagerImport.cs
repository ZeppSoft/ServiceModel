using ServiceModel.Grpc.DesignTime;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [ImportGrpcService(typeof(ILoanService))]
    internal static partial class LoanManagerImport
    {
    }
}
