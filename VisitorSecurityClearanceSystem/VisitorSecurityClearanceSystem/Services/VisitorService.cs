﻿using System.Reflection.Metadata;
using System.Xml.Linq;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;
using VisitorSecurityClearanceSystem.Interface;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using AutoMapper;
using Newtonsoft.Json;
using VisitorSecurityClearanceSystem.Common;

namespace VisitorSecurityClearanceSystem.Services
{
    public class VisitorService: IVisitorService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;
        public VisitorService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;

        }

        public async Task<VisitorDTO> AddVisitor(VisitorDTO visitorModel)
        {
            var existingVisitor = await _cosmoDBService.GetVisitorByEmail(visitorModel.Email);
            if (existingVisitor != null)
            {
                throw new InvalidOperationException("A visitor with the provided email already exists.");
            }

            var visitorEntity = _autoMapper.Map<VisitorEntity>(visitorModel);

            visitorEntity.Intialize(true, "visitor", "Atul", "Atul");

            var response = await _cosmoDBService.Add(visitorEntity);

            string subject = "Visitor Registration Approval Request";
            string toEmail = "atul2003@gmail.com";  
            string userName = "Manager";

            string message = $"Dear {userName},\n\n" +
                             $"A new visitor has registered and is awaiting your approval.\n\n" +
                             $"Visitor Details:\n" +
                             $"Name: {visitorModel.Name}\n" +
                             $"Contact Number: {visitorModel.Phone}\n" +
                             $"Email: {visitorModel.Email}\n" +
                             $"Purpose of Visit: {visitorModel.Purpose}\n\n" +
                             "Please review the details and approve or reject the request.\n\n" +
                             "Thank you,\nVisitor Management System";

            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmail(subject, toEmail, userName, message);

            return _autoMapper.Map<VisitorDTO>(response);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitors()
        {
            var visitors = await _cosmoDBService.GetAllVisitor();
            return visitors.Select(MapEntityToDTO).ToList();
        }

        public async Task<VisitorDTO> GetVisitorById(string id)
        {
            var visitor = await _cosmoDBService.GetVisitorById(id); 
            return _autoMapper.Map<VisitorDTO>(visitor);
        }

        public async Task<List<VisitorDTO>> GetVisitorsByStatus(bool status)
        {
            var visitors = await _cosmoDBService.GetVisitorByStatus(status);
            var visitorDTOs = visitors.Select(MapEntityToDTO).ToList();
            return visitorDTOs;
        }


        public async Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel)
        {
            var visitorEntity = await _cosmoDBService.GetVisitorById(id);
            if (visitorEntity == null)
            {
                throw new Exception("Manager not found");
            }
            visitorEntity = _autoMapper.Map<VisitorEntity>(visitorModel); ;
            visitorEntity.Id = id;
            var response = await _cosmoDBService.Update(visitorEntity);
            return _autoMapper.Map<VisitorDTO>(response);
        }

        public async Task<VisitorDTO> UpdateVisitorStatus(string visitorId, bool newStatus)
        {
            var visitor = await _cosmoDBService.GetVisitorById(visitorId);
            if (visitor == null)
                throw new Exception("Visitor not found");

            visitor.PassStatus = newStatus;
            await _cosmoDBService.Update(visitor);

            string subject = "Your Visitor Status Has Been Updated";
            string toEmail = visitor.Email; 
            string userName = visitor.Name;

            string message = $"Dear {userName},\n\n" +
                             $"We wanted to inform you that your visitor status has been updated.\n\n" +
                             $"New Status: {newStatus}\n\n" +
                             "If you have any questions or need further assistance, please contact us.\n\n" +
                             "Thank you,\nVisitor Management System";

            byte[] pdfBytes = null;
            if (newStatus)
            {
                pdfBytes = GenerateVisitorPassPdf(visitor);
            }

            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmail(subject, toEmail, userName, message, pdfBytes);

            return new VisitorDTO
            {
                Id = visitor.Id,
                Name = visitor.Name,
                Email = visitor.Email,
                PassStatus = visitor.PassStatus,
            };
        }

        private byte[] GenerateVisitorPassPdf(VisitorEntity visitor)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);
                XFont normalFont = new XFont("Arial", 12);

                gfx.DrawString("Visitor Pass", titleFont, XBrushes.Black, new XRect(0, 20, page.Width.Point, page.Height.Point), XStringFormats.Center);

                int yOffset = 60;
                gfx.DrawString($"Name: {visitor.Name}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Email: {visitor.Email}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Phone: {visitor.Phone}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Purpose of Visit: {visitor.Purpose}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);

                document.Save(ms);
                ms.Position = 0;

                return ms.ToArray();
            }
        }

        public async Task DeleteVisitor(string id)
        {
            await _cosmoDBService.DeleteVisitor(id);
        }

        private VisitorEntity MapDTOToEntity(VisitorDTO visitorModel)
        {
            return new VisitorEntity
            {
                Id = visitorModel.Id,
                Name = visitorModel.Name,
                Email = visitorModel.Email,
                Phone = visitorModel.Phone,
                Address = visitorModel.Address,
                CompanyName = visitorModel.CompanyName,
                Purpose = visitorModel.Purpose,
                EntryTime = visitorModel.EntryTime,
                ExitTime = visitorModel.ExitTime,
                Role= "visitor",
                PassStatus = false,
            };
        }

        private VisitorDTO MapEntityToDTO(VisitorEntity visitorEntity)
        {
            if (visitorEntity == null) return null;
            return new VisitorDTO
            {
                Id = visitorEntity.Id,
                Name = visitorEntity.Name,
                Email = visitorEntity.Email,
                Phone = visitorEntity.Phone,
                Address = visitorEntity.Address,
                CompanyName = visitorEntity.CompanyName,
                Purpose = visitorEntity.Purpose,
                EntryTime = visitorEntity.EntryTime,
                ExitTime = visitorEntity.ExitTime,
                Role = "visitor",
                PassStatus = false
            };
        }

        public async Task<VisitorDTO> AddVisitorByMakePostRequest(VisitorDTO visitor)
        {
            var serialObj = JsonConvert.SerializeObject(visitor);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl,Credentials.AddEmployeeEndPoint,serialObj);
            var responseObj = JsonConvert.DeserializeObject<VisitorDTO>(requestObj);
            return responseObj;

        }

        public async Task<IEnumerable<VisitorDTO>> GetAllEmployees()
        {
            var responseString = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetAllEmployeesEndPoint);
            var employees = JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(responseString);
            return employees;
        }
    }
}