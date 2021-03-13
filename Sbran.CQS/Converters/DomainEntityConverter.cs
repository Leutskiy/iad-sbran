using Sbran.CQS.Read.Results;
using System.Collections.Generic;
using Sbran.Domain.Entities;
using Sbran.Domain.Entities.System;
using Sbran.Domain.Entities.Chat;
using System;

namespace Sbran.CQS.Converters
{
    public static class DomainEntityConverter
    {
        public static ProfileResult ConvertToResult(Profile profile)
        {
            return new ProfileResult
            {
                Id = profile.Id,
                Avatar = profile.Photo,
                WebPages = profile.WebPages
            };
        }

        public static ContactResult ConvertToResult(Contact contact)
        {
            return new ContactResult
            {
                Id = contact.Id,
                Email = contact.Email,
                Postcode = contact.Postcode,
                HomePhoneNumber = contact.HomePhoneNumber,
                WorkPhoneNumber = contact.WorkPhoneNumber,
                MobilePhoneNumber = contact.MobilePhoneNumber
            };
        }

        public static PassportResult ConvertToResult(Passport passport)
        {
            return new PassportResult
            {
                Id = passport.Id,
                NameRus = passport.NameRus,
                NameEng = passport.NameEng,
                SurnameRus = passport.SurnameRus,
                SurnameEng = passport.SurnameEng,
                PatronymicNameRus = passport.PatronymicNameRus,
                PatronymicNameEng = passport.PatronymicNameEng,
                BirthPlace = passport.BirthPlace,
                BirthCountry = passport.BirthCountry,
                Citizenship = passport.Citizenship,
                Residence = passport.Residence,
                ResidenceCountry = passport.ResidenceCountry,
                ResidenceRegion = passport.ResidenceRegion,
                IdentityDocument = passport.IdentityDocument,
                IssuePlace = passport.IssuePlace,
                DepartmentCode = passport.DepartmentCode,
                BirthDate = passport.BirthDate,
                IssueDate = passport.IssueDate,
                Gender = passport.Gender
            };
        }

        public static DocumentResult ConvertToResult(Document document)
        {
            return new DocumentResult
            {
                Id = document.Id,
                Name = document.Name,
                Content = document.Content,
                UpdateDate = document.UpdateDate,
                CreatedDate = document.CreatedDate,
                DocumentType = document.DocumentType
            };
        }

        public static VisitDetailResult ConvertToResult(VisitDetail visitDetail)
        {
            return new VisitDetailResult
            {
                Id = visitDetail.Id,
                Goal = visitDetail.Goal,
                Country = visitDetail.Country,
                VisaType = visitDetail.VisaType,
                VisaCity = visitDetail.VisaCity,
                VisaCountry = visitDetail.VisaCountry,
                VisitingPoints = visitDetail.VisitingPoints,
                VisaMultiplicity = visitDetail.VisaMultiplicity,
                PeriodInDays = visitDetail.PeriodDays,
                ArrivalDate = visitDetail.ArrivalDate,
                DepartureDate = visitDetail.DepartureDate,
            };
        }

        public static StateRegistrationResult ConvertToResult(StateRegistration stateRegistration)
        {
            return new StateRegistrationResult
            {
                Id = stateRegistration.Id,
                Inn = stateRegistration.Inn ?? string.Empty,
                Ogrnip = stateRegistration.Ogrnip ?? string.Empty
            };
        }

        public static AlienResult ConvertToResult(
            Alien alien,
            ContactResult? contactResult,
            PassportResult? passportResult,
            OrganizationResult? organizationResult,
            StateRegistrationResult? stateRegistrationResult)
        {
            return new AlienResult
            {
                Id = alien.Id,
                Contact = contactResult,
                Passport = passportResult,
                Organization = organizationResult,
                StateRegistration = stateRegistrationResult,
                Position = alien.Position,
                WorkPlace = alien.WorkPlace,
                WorkAddress = alien.WorkAddress,
                StayAddress = alien.StayAddress
            };
        }

        public static EmployeeResult ConvertToResult(
            Employee employee,
            ContactResult? contactResult,
            PassportResult? passportResult,
            OrganizationResult? organizationResult,
            StateRegistrationResult? stateRegistrationResult)
        {
            return new EmployeeResult
            {
                Id = employee.Id,
                Contact = contactResult,
                Passport = passportResult,
                Organization = organizationResult,
                StateRegistration = stateRegistrationResult,
                ManagerId = employee.ManagerId,
                AcademicDegree = employee.AcademicDegree,
                AcademicRank = employee.AcademicRank,
                Education = employee.Education,
                Position = employee.Position,
                WorkPlace = employee.WorkPlace
            };
        }

        public static OrganizationResult ConvertToResult(
            Organization organization,
            StateRegistrationResult? stateRegistrationResult)
        {
            return new OrganizationResult
            {
                Id = organization.Id,
                Name = organization.Name,
                ShortName = organization.ShortName,
                StateRegistration = stateRegistrationResult,
                LegalAddress = organization.LegalAddress,
                ScientificActivity = organization.ScientificActivity
            };
        }

        public static ForeignParticipantResult ConvertToResult(ForeignParticipant foreignParticipant)
        {
            return new ForeignParticipantResult
            {
                Id = foreignParticipant.Id,
                Passport = foreignParticipant.Passport == null ? null : ConvertToResult(foreignParticipant.Passport)
            };
        }

        public static InvitationResult ConvertToResult(
            Invitation invitation,
            AlienResult? alienResult,
            EmployeeResult? employeeResult,
            VisitDetailResult? visitDetailResult,
            IEnumerable<ForeignParticipantResult>? foreignParticipantResultCollection)
        {
            return new InvitationResult
            {
                Id = invitation.Id,
                Alien = alienResult,
                Employee = employeeResult,
                VisitDetail = visitDetailResult,
                ForeignParticipants = foreignParticipantResultCollection
            };
        }

        public static ChatRoomResult ConvertToResult(
            User user,
            ChatRoomList chatRoomList,
            ChatMessage chatMessage
            )
        {
            return new ChatRoomResult
            {
                userid = user.Id,
                account = user.Account ?? "",
                //Пока что хардкор, в след раз поменяю на Base64
                image = "../../../assets/images/profilephotos/lora.jpg",
                chatRoomId = chatRoomList?.ChatRoomId ?? Guid.NewGuid(),
                lastmessage = chatMessage?.Message ?? "",
                lastmessagedate = chatMessage?.DateTime ?? null,
            };
        }

        public static ChatMessageResult ConvertToResult(
            User user,
            User userTo,
            ChatMessage chatMessage
            )
        {
            return new ChatMessageResult
            {
                Message = chatMessage.Message ?? "",
                DateTime = chatMessage.DateTime,
                profileTo = userTo.ProfileId,
                profileId = user.ProfileId
            };
        }
    }
}