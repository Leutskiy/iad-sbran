import { NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";
import { join } from "path";

export interface LoginData {
  login: string,
  password: string
}

export interface RegistrationData {
  login: string,
  password: string,
  confirmPassword: string
}

export class User {
  id: number;
  username: string;
  password: string;
  profileId?: number;
  token?: string;
}

export class Contact {
  id: string;
  email: string | null;
  postcode: string | null;
  homePhoneNumber: string | null;
  workPhoneNumber: string | null;
  mobilePhoneNumber: string | null;

  constructor() {
    this.id = "";
    this.email = "";
    this.postcode = "";
    this.homePhoneNumber = "";
    this.workPhoneNumber = "";
    this.mobilePhoneNumber = "";
  }

  public init(
    id: string,
    email: string | null,
    postcode: string | null,
    homePhoneNumber: string | null,
    workPhoneNumber: string | null,
    mobilePhoneNumber: string | null): void {
    this.id = id;
    this.email = email;
    this.postcode = postcode;
    this.homePhoneNumber = homePhoneNumber;
    this.workPhoneNumber = workPhoneNumber;
    this.mobilePhoneNumber = mobilePhoneNumber;
  }
}

export class Job {
  workPlace: string | null;
  position: string | null;

  constructor() {
    this.workPlace = "";
    this.position = "";
  }

  public init(
    workPlace: string | null,
    position: string | null): void {
    this.workPlace = workPlace;
    this.position = position;
  }
}

export class ScientificInfo {
  academicDegree: string | null;
  academicRank: string | null;
  education: string | null;

  constructor() {
    this.academicDegree = "";
    this.academicRank = "";
    this.education = "";
  }

  public init(
    academicDegree: string | null,
    academicRank: string | null,
    education: string | null): void {
    this.academicDegree = academicDegree;
    this.academicRank = academicRank;
    this.education = education;
  }
}

export class Profile {
  id: string;
  userId: string;
  avatar: any[];
  webPages: string[] | null;

  constructor() {
    this.id = "";
    this.userId = "";
    this.avatar = [];
    this.webPages = null;
  }

  public init(
    id: string,
    userId: string,
    avatar: any[],
    webPages: string[] | null): void {
    this.id = id;
    this.userId = userId;
    this.avatar = avatar;
    this.webPages = webPages;
  }
}

export class UserInfo {
  private defaultFieldValue: string = "Не заполнено";
  private defaulAvatarValue: string = "assets/images/avatar.jpg";

  profile: Profile;
  scientificInterests: string | null;
  consularOffices: string | null;
  memberships: string | null;
  fio: string | null;
  fax: string | null;
  academicDegree: string | null;
  academicRank: string | null;
  education: string | null;
  shortName: string | null;
  workPlace: string | null;
  position: string | null;
  email: string | null;
  mobilePhoneNumber: string | null;
  avatarContent: string | null;

  invitesCount: number | null;
  departuresCount: number | null;
  publishsCount: number | null;
  membershipsCount: number | null;

  constructor() {
    this.profile = new Profile();
    this.fio = this.defaultFieldValue;
    this.fax = this.defaultFieldValue;
    this.academicDegree = this.defaultFieldValue;
    this.academicRank = this.defaultFieldValue;
    this.education = this.defaultFieldValue;
    this.shortName = this.defaultFieldValue;
    this.workPlace = this.defaultFieldValue;
    this.position = this.defaultFieldValue;
    this.email = this.defaultFieldValue;
    this.mobilePhoneNumber = this.defaultFieldValue;
    this.avatarContent = null;
    this.invitesCount = 0;
    this.departuresCount = 0;
    this.publishsCount = 0;
    this.membershipsCount = 0;
    this.scientificInterests = "";
    this.consularOffices = "";
    this.memberships = "";
  }

  public init(
    fio: string | null,
    fax: string | null,
    avatarBase64String: string | null,
    academicDegree: string | null,
    academicRank: string | null,
    education: string | null,
    shortName: string | null,
    workPlace: string | null,
    position: string | null,
    email: string | null,
    mobilePhoneNumber: string | null,
    invitesCount: number | null,
    departuresCount: number | null,
    publishsCount: number | null,
    membershipsCount: number | null,
    scientificInterests: string | null,
    consularOffices: string | null,
    memberships: string | null,
  ): void {
    this.mobilePhoneNumber = mobilePhoneNumber ? mobilePhoneNumber : this.mobilePhoneNumber;
    this.academicDegree = academicDegree ? academicDegree : this.academicDegree;
    this.academicRank = academicRank ? academicRank : this.academicRank;
    this.education = education ? education : this.education;
    this.shortName = shortName ? shortName : this.shortName;
    this.workPlace = workPlace ? workPlace : this.workPlace;
    this.position = position ? position : this.position;
    this.avatarContent = avatarBase64String
      ? `data:image/jpeg;base64,${avatarBase64String}`
      : this.defaulAvatarValue;
    this.email = email ? email : this.email;
    this.fio = fio ? fio : this.fio;
    this.fax = fax ? fax : this.fax;
    this.invitesCount = invitesCount ? invitesCount : this.invitesCount;
    this.departuresCount = departuresCount ? departuresCount : this.departuresCount;
    this.publishsCount = publishsCount ? publishsCount : this.publishsCount;
    this.membershipsCount = membershipsCount ? membershipsCount : this.membershipsCount;

    this.scientificInterests = scientificInterests ? scientificInterests : this.scientificInterests;
    this.consularOffices = consularOffices ? consularOffices : this.consularOffices;
    this.memberships = memberships ? memberships : this.memberships;
  }
}

export class StateRegistration {
  id: string;
  inn: string | null;
  ogrnip: string | null;

  constructor() {
    this.id = "";
    this.inn = "";
    this.ogrnip = "";
  }

  public init(
    id: string,
    inn: string | null,
    ogrnip: string | null): void {
    this.id = id;
    this.inn = inn;
    this.ogrnip = ogrnip;
  }
}

export class Organization {
  id: string;
  name: string | null;
  shortName: string | null;
  legalAddress: string | null;
  scientificActivity: string | null;
  stateRegistration: StateRegistration | null

  constructor() {
    this.id = "";
    this.name = "";
    this.shortName = "";
    this.legalAddress = "";
    this.stateRegistration = new StateRegistration();
  }

  public init(
    id: string,
    name: string | null,
    shortName: string | null,
    legalAddress: string | null,
    scientificActivity: string | null,
    stateRegistrationId: string | null,
    inn: string | null,
    ogrnip: string | null): void {
    this.id = id;
    this.name = name;
    this.shortName = shortName;
    this.legalAddress = legalAddress;
    this.scientificActivity = scientificActivity;
    this.stateRegistration.init(stateRegistrationId, inn, ogrnip);
  }
}

export class Employee {
  id: string;
  passport: Passport | null;
  contact: Contact | null;
  organization: Organization | null;
  job: Job | null;
  scientificInfo: ScientificInfo | null;
  stateRegistration: StateRegistration | null;

  constructor() {
    this.id = "";
    this.scientificInfo = new ScientificInfo();
    this.job = new Job();
    this.passport = new Passport();
    this.contact = new Contact();
    this.organization = new Organization();
    this.stateRegistration = new StateRegistration();
  }

  public init(
    id: string,
    academicDegree: string | null,
    academicRank: string | null,
    education: string | null,
    workPlace: string | null,
    position: string | null): void {
    this.id = id;
    this.job.init(workPlace, position);
    this.scientificInfo.init(academicDegree, academicRank, education);
  }
}

export class MessageDto {
  public user: string = '';
  public messageText: string = '';
}


export enum Gender {
  male,
  female,
  empty
}

export enum VisaMultiplicity {
  single,
  double,
  multiple
}

export enum FinancialCondition {
  host,
  accepted
}

export interface Passport2 {
  id: string;
  nameRus: string;
  nameEng: string;
  surnameRus: string;
  surnameEng: string;
  patronymicNameRus: string;
  patronymicNameEng: string;
  birthPlace: string;
  birthCountry: string;
  citizenship: string;
  residence: string;
  residenceCountry: string;
  residenceRegion: string;
  identityDocument: string;
  issuePlace: string;
  departmentCode: string;
  birthDate: Date;
  issueDate: Date;
  gender: number;
}

export class Passport {
  id: string;
  nameRus: string | null;
  nameEng: string | null;
  surnameRus: string | null;
  surnameEng: string | null;
  patronymicNameRus: string | null;
  patronymicNameEng: string | null;
  gender: Gender | number | null;
  birthDate: Date | string | null;
  birthPlace: string | null;
  birthCountry: string | null;
  residence: string | null;
  citizenship: string | null;
  residenceRegion: string | null;
  residenceCountry: string | null;
  issueDate: Date | string | null;
  issuePlace: string | null;
  departmentCode: string | null;
  identityDocument: string | null;

  constructor() {
    this.id = "";
    this.nameRus = "";
    this.nameEng = "";
    this.surnameRus = "";
    this.surnameEng = "";
    this.patronymicNameRus = "";
    this.patronymicNameEng = "";
    this.gender = null;
    this.birthDate = null;
    this.birthPlace = "";
    this.birthCountry = "";
    this.residence = "";
    this.citizenship = "";
    this.residenceRegion = "";
    this.residenceCountry = "";
    this.issueDate = null;
    this.issuePlace = "";
    this.departmentCode = "";
    this.identityDocument = "";
  }

  public init(
    id: string,
    nameRus: string | null,
    nameEng: string | null,
    surnameRus: string | null,
    surnameEng: string | null,
    patronymicNameRus: string | null,
    patronymicNameEng: string | null,
    gender: Gender | number | null,
    birthDate: string | null,
    birthPlace: string | null,
    birthCountry: string | null,
    residence: string | null,
    citizenship: string | null,
    residenceRegion: string | null,
    residenceCountry: string | null,
    issueDate: string | null,
    issuePlace: string | null,
    departmentCode: string | null,
    identityDocument: string | null): void {
    this.id = id;
    this.nameRus = nameRus;
    this.nameEng = nameEng;
    this.surnameRus = surnameRus;
    this.surnameEng = surnameEng;
    this.patronymicNameRus = patronymicNameRus;
    this.patronymicNameEng = patronymicNameEng;
    this.gender = gender;
    this.birthDate = birthDate;
    this.birthPlace = birthPlace;
    this.birthCountry = birthCountry;
    this.residence = residence;
    this.citizenship = citizenship;
    this.residenceRegion = residenceRegion;
    this.residenceCountry = residenceCountry;
    this.issueDate = issueDate;
    this.issuePlace = issuePlace;
    this.departmentCode = departmentCode;
    this.identityDocument = identityDocument;
  }
}

/* Описание DTO для просмотра и изменения данных*/

// DTO для создания приглашения
export class InviteeDto {
  public alienJob: Job | null;
  public alienContact: Contact | null;
  public alienPassport: Passport | null;
  public alienOrganization: Organization | null;
  public alienScientificInfo: ScientificInfo | null;
  public alienStateRegistration: StateRegistration | null;
  public alienWorkAddress: string | null;
  public alienStayAddress: string | null;
}

export class Alien {
  id: string;
  contact: Contact | null;
  passport: Passport | null;
  organization: Organization | null;
  stateRegistration: StateRegistration | null;
  position: string | null;
  workPlace: string | null;
  workAddress: string | null;
  stayAddress: string | null;

  constructor() {
    this.id = "";
    this.contact = new Contact();
    this.passport = new Passport();
    this.organization = new Organization();
    this.stateRegistration = new StateRegistration();
  }
}

export class VisitDetail {
  id: string;
  goal: string | null;
  country: string | null;
  visitingPoints: string | null;
  periodInDays: number | null;
  arrivalDate: Date | string | null;
  departureDate: Date | string | null;
  visaType: string | null;
  visaCity: string | null;
  visaCountry: string | null;
  visaMultiplicity: VisaMultiplicity | null;
  financialCondition: FinancialCondition | null;
  typeReception: string | null;
}

export class ForeignParticipant {
  id: string;
  passport: Passport | null;

  constructor() {
    this.id = "";
    this.passport = new Passport();
  }
}

export class Invitation {
  id: string;
  reportId: string | null;
  invitationStatus: InvitationStatus | null;
  status: string | null;
  alien: Alien;
  employee: Employee;
  visitDetail: VisitDetail | null;
  foreignParticipants: ForeignParticipant[] | null;

  constructor() {
    this.id = "";
    this.alien = new Alien();
    this.reportId = "";
    this.invitationStatus = InvitationStatus.Creating;
    this.employee = new Employee();
    this.visitDetail = new VisitDetail();
    this.foreignParticipants = [];
  }
}

export class NewInvitationDto {
  alien: InviteeDto;
  visitDetail: VisitDetail | null;
  foreignParticipants: ForeignParticipant[] | null;

  constructor() {
    this.alien = new InviteeDto();
    this.visitDetail = new VisitDetail();
    this.foreignParticipants = [];
  }
}


export class ChatRoomsInfo {

  private defaulAvatarValue: string = "assets/images/avatar.jpg";
  account: string | null;
  image: string | null;
  userId: string | null;
  chatRoomId: string | null;
  lastmessagedate: Date | string | null;
  lastmessage: string | null;

  constructor() {
    this.account = this.account;
    this.image = this.image;
    this.chatRoomId = this.chatRoomId;
    this.userId = this.userId;
    this.lastmessage = this.lastmessage;
    this.lastmessagedate = this.lastmessagedate;
  }

  public init(
    userid: string | null,
    image: string | null,
    userfullname: string | null,
    lastmessage: string | null,
    chatRoomId: string | null,
    lastmessagedate: string | null): void {
    this.account = userid ? userid : this.account;
    this.image = image ?
      `data:image/jpeg;base64,${image}`
      : this.defaulAvatarValue;;
    this.chatRoomId = chatRoomId ? chatRoomId : this.chatRoomId;
    this.userId = userfullname ? userfullname : this.userId;
    this.lastmessage = lastmessage ? lastmessage : this.lastmessage;
    this.lastmessagedate = lastmessagedate ? lastmessagedate : this.lastmessagedate;
  }
}

export class MyMessagesInRoom {

  isValid: boolean | null;
  message: string | null;
  dateTime: Date | string | null;
  profileId: string | null;
  profileTo: string | null;
  isFile: boolean | null;
  fileId: string | null;
  fileName: string | null;

  constructor() {
    this.isValid = this.isValid;
    this.message = this.message;
    this.dateTime = this.dateTime;
    this.profileId = this.profileId;
    this.profileTo = this.profileTo;
    this.isFile = this.isFile;
    this.fileId = this.fileId;
    this.fileName = this.fileName;
  }

  public init(
    isValid: boolean | null,
    message: string | null,
    profileId: string | null,
    profileTo: string | null,
    isFile: boolean | null,
    fileId: string | null,
    fileName: string | null,
    dateTime: string | null): void {
    this.isValid = isValid ? isValid : this.isValid;
    this.message = message ? message : this.message;
    this.profileId = profileId ? profileId : this.profileId;
    this.dateTime = dateTime ? dateTime : this.dateTime;
    this.profileTo = profileTo ? profileTo : this.profileTo;
    this.isFile = isFile ? isFile : this.isFile;
    this.fileId = fileId ? fileId : this.fileId;
    this.fileName = fileName ? fileName : this.fileName;
  }
}

export class Message {
  account: string;
  chatRoomId: string;
  message: string;
  length: number;

  constructor() {
    this.account = "";
    this.chatRoomId = "";
    this.message = "";
    this.length = this.message.length;
  }

  public init(
    account: string,
    chatRoomId: string,
    message: string) {
    this.account = account;
    this.chatRoomId = chatRoomId;
    this.message = message;
    this.length = this.message.length;
  }
}

export class Departure {
  id: string | null;
  sendingCountry: string | null;
  cityOfBusiness: string | null;
  sourceOfFinancing: string | null;
  basicOfDeparture: string | null;
  hostOrganization: string | null;
  placeOfResidence: string | null;
  purposeOfTheTrip: string | null;
  status: string | null;
  justificationOfTheBusiness: string | null;
  dateStart: Date | string | null;
  dateEnd: Date | string | null;
  departureStatus: DepartureStatus | null;
  reportId: string | null;
  employeeId: string | null;
  report: Report | null;
}


export class ConsularOffice {
  id: string | null;
  nameOfTheConsularPost: string | null;
  countryOfLocation: string | null;
  cityOfLocation: string | null;
  employeeId: string | null;
}


export class InternationalAgreement {
  id: string | null;
  theNameOfTheAgreement: string | null;
  theFirstPartyToTheAgreement: string | null;
  theSecondPartyToTheAgreement: string | null;
  placeOfSigning: string | null;
  textOfTheAgreement: string | null;
  dateOfEntry: Date | string | null;
  employeeId: string | null;
}


export class Membership {
  id: string | null;
  nameOfCompany: string | null;
  statusInTheOrganization: string | null;
  siteOfTheOrganization: string | null;
  dateOfEntry: Date | string | null;
  membershipType: MembershipType | null;
  siteOfJournal: string | null;
  employeeId: string | null;

  constructor() {
    this.siteOfJournal = "";
  }
}

export enum MembershipType {
  russian,
  other
}

export class Publication {
  id: string | null;
  abstract: string | null;
  keywords: string | null;
  literature: string | null;
  employeeId: string | null; // TODO: костыль
  scientificAdvisor: string | null;
  titleOfTheArticle: string | null;
  mainTextOfTheArticle: string | null;
}


export class ScientificInterests {
  id: string | null;
  nameOfScientificInterests: string | null;
  employeeId: string | null; // TODO: костыль
}


export enum ReportType {
  /// <summary>
  /// Приглашение
  /// </summary>
  Invition,
  /// <summary>
  /// Выезд
  /// </summary>
  Departure
}

export enum DepartureStatus {
  /// <summary>
  /// Не согласовано
  /// </summary>
  NonAgreement,

  /// <summary>
  /// Согласовано
  /// </summary>
  Agreement
}

export enum InvitationStatus {
  /// <summary>
  /// Создание
  /// </summary>
  Creating,

  /// <summary>
  /// Отправка в МВД
  /// </summary>
  Sending,

  /// <summary>
  /// Согласовано
  /// </summary>
  Agreement
}


export class Report {
  id: string | null;
  mainPart: string | null;
  findings: string | null;
  suggestion: string | null;
  foreignInterest: string | null;
  status: boolean | null;
  appendix: Appendix[] | null;
  listOfScientists: ListOfScientist[] | null;

  reportType: ReportType;
  parentId: string | null;

  constructor() {
    this.mainPart = "";
    this.findings = "";
    this.suggestion = "";
    this.foreignInterest = "";

    this.appendix = Array<Appendix>();
    this.listOfScientists = Array<ListOfScientist>();
  }
}

export class Appendix {
  id: string | null;
  fileBinary: string;
  fileName: string;
  reportId: string | null;
}

export class ListOfScientist {
  id: string | null;
  fullName: string | null;
  workPlace: string | null;
  position: string | null;
  email: string | null;
  phoneNumber: string | null;
  academicDegree: string | null;
  type: boolean;
  reportId: string;
}

export class HomePage {
  id: string | null;
  news: any[] | null;
  employees: any[] | null;
  voteLists: any[] | null;
  countEmployees: number | null;
  countOnlineEmployee: number | null;
}

export class News {
  id: string | null;
  name: string | null;
  dateTime: Date | null;
}

export class Vote {
  id: string | null;
  name: string | null;
  voteLists: VoteList[] | null;
}

export class VoteList {
  id: string | null;
  name: string | null;
  count: number | null;
  voteId: number | null;
}

