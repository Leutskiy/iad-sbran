using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace Sbran.WebApp.Controllers
{
    // TODO: inviter --- invitee
    // TODO: перенести invitationId  в конец пути
    /// <summary>
    /// Контроллер приглашений
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1")]
    public class InvitationController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IInternationalAgreementRepository _iInternationalAgreementRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly InvitationWriteCommand _invitationWriteCommand;
        private readonly IReadCommand<InvitationResult> _invitationReadCommand;
        private IWebHostEnvironment _environment;

        public InvitationController(
            IEmployeeRepository employeeRepository,
            InvitationWriteCommand invitationWriteCommand,
            IReadCommand<InvitationResult> invitationReadCommand,
            IInvitationRepository invitationRepository,
           IInternationalAgreementRepository iInternationalAgreementRepository,
            IWebHostEnvironment environment)
        {
            _employeeRepository = employeeRepository;
            _invitationReadCommand = invitationReadCommand;
            _invitationWriteCommand = invitationWriteCommand;
            _invitationRepository = invitationRepository;
            _environment = environment;
            _iInternationalAgreementRepository = iInternationalAgreementRepository;
        }

        #region Invitation

        [HttpGet]
        [Route("invitation/{invitationId:guid}")]
        public async Task<InvitationResult> GetById(Guid invitationId)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationReadCommand.ExecuteAsync(invitationId);
            invitation.Employee.Invitations = null;
            invitation.Employee.ScientificInterests = null;
            invitation.Employee.ConsularOffices = null;
            invitation.Employee.Publications = null;
            invitation.Employee.Memberships = null;
            invitation.Employee.Departures = null;
            return invitation;
        }

        // TODO: унести в котроллер сотруднику
        [HttpGet]
        [Route("employee/{employeeId:guid}/invitations")]
        public async Task<List<InvitationResult>> GetAll(Guid employeeId)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            var employee = await _employeeRepository.GetAsync(employeeId);

            var invitationResults = new List<InvitationResult>();
            var invitations = employee.Invitations;

            foreach (var invitation in invitations)
            {
                var invitationResult = await _invitationReadCommand.ExecuteAsync(invitation.Id);
                invitationResult.Employee.Invitations = null;
                invitationResult.Employee.ScientificInterests = null;
                invitationResult.Employee.ConsularOffices = null;
                invitationResult.Employee.Publications = null;
                invitationResult.Employee.Memberships = null;
                invitationResult.Employee.Departures = null;
                invitationResults.Add(invitationResult);
            }

            return invitationResults;
        }

        // TODO: унести в контроллер сотруднику
        // TODO: переделать метод для добавления приглашения для рабочего; скоре всего надо передать сюда полное DTO вместо InviteeDto
        [HttpPost]
        [Route("employee/{employeeId:guid}/invitation")]
        public Task<Guid> AddInvitation(Guid employeeId, InvitationDto invitationDto)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _invitationWriteCommand.AddForEmployeeAsync(employeeId, invitationDto);
        }

        #endregion

        #region VisitDetail

        [HttpPut]
        [Route("invitation/{invitationId:guid}/visitdetails")]
        public Task<Guid> UpdateVisitDetailAsync(Guid invitationId, VisitDetailDto visitDetailDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.AddOrUpdateVisitDetailAsync(invitationId, visitDetailDto);
        }

        #endregion

        #region Alien

        // TODO: надо понять, что делать с JobDto / AlienJobDto
        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/job")]
        public Task<Guid> UpdateAlienStateRegistrationAsync(Guid invitationId, AlienJobDto alienJobDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.UpdateAlienJobAsync(invitationId, alienJobDto);
        }

        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/passport")]
        public Task<Guid> UpdateAlienPassportAsync(Guid invitationId, PassportDto passportDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.AddOrUpdatePassportAsync(invitationId, passportDto);
        }

        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/contact")]
        public Task<Guid> UpdateAlienContactAsync(Guid invitationId, ContactDto contactDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.AddOrUpdateContactAsync(invitationId, contactDto);
        }

        /* TODO: надо подумать насчет работы для иностранца
		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/job")]
		public Task<Guid> UpdateAlienJobAsync(Guid invitationId, AlienJobDto jobDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateJobAsync(invitationId, jobDto);
		}
		*/

        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/organization")]
        public Task<Guid> UpdateAlienOrganizationAsync(Guid invitationId, OrganizationDto organizationDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.AddOrUpdateOrganizationAsync(invitationId, organizationDto);
        }

        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/stateregistration")]
        public Task<Guid> UpdateAlienStateRegistrationAsync(Guid invitationId, StateRegistrationDto stateRegistrationDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.AddOrUpdateStateRegistrationAsync(invitationId, stateRegistrationDto);
        }

        [HttpPut]
        [Route("invitation/{invitationId:guid}/alien/{stayaddress}")]
        public Task<Guid> UpdateAlienStayAddressAsync(Guid invitationId, string stayAddress)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            return _invitationWriteCommand.UpdateAlienStayAddressAsync(invitationId, stayAddress);
        }

        #endregion


        [HttpGet]
        [Route("invitation/{id:guid}/agree")]
        public async Task<IActionResult> Agree(Guid id)
        {
            await _invitationRepository.Agree(id);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("invitation/{id:guid}/report")]
        public async Task<IActionResult> Report(Guid id)
        {
            var invitation = await _invitationReadCommand.ExecuteAsync(id);
            if (invitation == null)
            {
                return NotFound();
            }
            MemoryStream workStream = new MemoryStream();
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 35f, 50f, 25f, 15f);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
            writer.CloseStream = false;
            document.Open();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font engfont = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font smalfont = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontunder = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.UNDERLINE);
            iTextSharp.text.Font fontbold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font smallfontbold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);

            Paragraph p = new Paragraph("\nСО РАН\n", font);
            p.Alignment = Element.ALIGN_LEFT;
            document.Add(p);
            var organization = invitation?.Alien?.Organization?.ShortName ?? "не указано";
            organization += "  " + invitation?.Alien?.Organization?.LegalAddress ?? "не указано";
            p = new Paragraph("Наименование и адрес консульского учреждения, в зависимости от гражданства приглашаемого:\n" + organization, fontbold);
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = document.PageSize.Width - 200;
            table.SpacingAfter = 10;
            table.SpacingBefore = 10;

            table.HorizontalAlignment = Element.ALIGN_CENTER;
            document.Add(table);

            table = new PdfPTable(new float[] { 2.5f, 2.5f, 3f, 2.5f });
            table.TotalWidth = document.PageSize.Width - 50;
            table.SpacingAfter = 10;
            // table.SpacingBefore = 10;
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell lcell = new PdfPCell();
            lcell.Border = 0; lcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            lcell.AddElement(new Phrase("Паспортные данные иностранца (из введенных данных на иностранца):", smalfont));
            lcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            table.AddCell(lcell);
            PdfPCell rcell = new PdfPCell();
            rcell.Border = 0;
            var bio = "Фамилия: " + invitation?.Alien?.Passport?.SurnameRus ?? "не указано";
            bio += ".\nИмя: " + invitation?.Alien?.Passport?.NameRus ?? "не указано";
            bio += ".\nПол: " + invitation?.Alien?.Passport?.Gender;
            bio += ".\nДата, государство и город рождения: " + invitation?.Alien?.Passport?.BirthDate?.ToString("g") ?? "не указано";
            bio += "," + invitation?.Alien?.Passport?.BirthCountry ?? "не указано";
            bio += ", " + invitation?.Alien?.Passport?.BirthPlace ?? "не указано";
            bio += ".\nГосударство и город постоянного проживания: " + invitation?.Alien?.Passport?.ResidenceCountry ?? "не указано";
            bio += " " + invitation?.Alien?.Passport?.ResidenceRegion ?? "не указано";
            bio += ".\nПаспорт: " + invitation?.Alien?.Passport?.IdentityDocument ?? "не указано";
            bio += ".\nСрок действия: " + invitation?.Alien?.Passport?.IssueDate ?? "не указано";
            bio += ".\nОрганизация, должность: " + invitation?.Alien?.Organization?.Name ?? "не указано";
            bio += " " + invitation?.Alien?.Position ?? "";
            bio += ".\nФдрес, телефон: " + invitation?.Alien?.StayAddress ?? "не указано";
            bio += " " + invitation?.Alien?.Contact?.MobilePhoneNumber ?? "не указано";
            bio += ".\nЦель поездки: " + invitation?.VisitDetail?.Goal ?? "не указано";
            bio += ".\nКратность визы: " + invitation?.VisitDetail?.VisaMultiplicity ?? "не указано";
            bio += ".\nПредполагаемый въезд в РФ: " + invitation?.VisitDetail?.ArrivalDate ?? "не указано";
            bio += ".\nНа срок: " + invitation?.VisitDetail?.DepartureDate ?? "не указано";
            bio += ".\nМесто предполагаемого проживания: " + invitation?.VisitDetail?.VisaCity ?? "не указано";
            bio += ".\nПункты посещения в РФ: " + invitation?.VisitDetail?.VisitingPoints ?? "не указано";

            rcell.AddElement(new Phrase(bio, smalfont));
            table.AddCell(rcell);
            var worldAgreement = await _iInternationalAgreementRepository.GetAgreementWithSecondName(invitation?.Alien?.Passport?.BirthCountry ?? "");
            var agreement = worldAgreement?.TextOfTheAgreement ?? "не указано";
            lcell = new PdfPCell();
            lcell.Border = 0; lcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            lcell.AddElement(new Phrase("«Основания для приглашения»: отображается наименование международного соглашения в зависимости от гражданства приглашенного (БД «приглашения» - данные иностранца) путем поиска страны в поле «вторая сторона соглашения» БД «международные соглашения»: " + agreement, smalfont));
            lcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            table.AddCell(lcell);
            rcell = new PdfPCell();
            rcell.Border = 0;
            rcell.AddElement(new Phrase("Должность начальника ОВС СО РАН", smalfont));
            table.AddCell(rcell);
            document.Add(table);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }
    }
}