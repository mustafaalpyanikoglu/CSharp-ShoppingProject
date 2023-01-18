using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.OperationClaims.Constants
{
    public static class OperationClaimMessages
    {
        public static string AddedOperationClaim = "added operation claim";
        public static string DeletedOperationClaim = "deleted operation claim";
        public static string UpdatedOperationClaim = "updated operation claim";
        public const string OperationClaimNameAlreadyExists = "operation claim name already exists";

        public static string OperationFailed = "operation failed";

        public static string OperationClaimAvaliable = "operation claim avaliable";
        public static string OperationClaimNotFound = "operation claim not found";
    }
}
