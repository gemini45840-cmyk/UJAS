using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;
using UJAS.Application.Common.DTOs;
using UJAS.Application.Common.Interfaces;
using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Field;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.FileStorage;

namespace UJAS.Application.Applications.Commands
{
    public class CreateApplicationCommand : IRequest<Guid>
    {
        public ApplicationCreateDto Application { get; set; }
        public Guid ApplicantId { get; set; }
        public bool IsDraft { get; set; }
        public string UserIpAddress { get; set; }
        public string UserAgent { get; set; }

        public CreateApplicationCommand(ApplicationCreateDto application, Guid applicantId)
        {
            Application = application;
            ApplicantId = applicantId;
        }
    }
}
