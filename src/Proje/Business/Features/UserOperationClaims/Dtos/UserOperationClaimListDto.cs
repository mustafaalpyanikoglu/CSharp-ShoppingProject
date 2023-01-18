﻿namespace Business.Features.UserOperationClaims.Dtos
{
    public class UserOperationClaimListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string OperationClaimName { get; set; }
        public string OperationClaimNameDescription { get; set; }
    }
}
