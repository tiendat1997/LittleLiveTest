using AutoMapper;
using LittleLive.Core;
using LittleLive.Core.Models;
using LittleLive.Core.Service;
using LittleLive.Core.Services;
using LittleLive.Service.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Entities = LittleLive.Core.Entities;
using Models = LittleLive.Core.Models;

namespace LittleLive.Service
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IMapper _mapper;
        public TrackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        private void AppendSheetForEachClass(ExcelWorksheets worksheets, Class classInfo, IEnumerable<Track> tracks)
        {
            var worksheet = worksheets.Add(classInfo.Name);
            worksheet.AppendHeader(typeof(ActivityExportHeader), 1);

            int rowIndex = 1;
            foreach (var track in tracks)
            {
                rowIndex++;
                worksheet.Cells[rowIndex, ActivityExportHeader.ChildName.ColumnIndex].Value = track.ChildName;
                worksheet.Cells[rowIndex, ActivityExportHeader.TimeCheckIn.ColumnIndex].Value = track.TimeCheckIn.ToLocalTime();
                worksheet.Cells[rowIndex, ActivityExportHeader.TimeCheckOut.ColumnIndex].Value = track.TimeCheckOut.ToLocalTime();
                worksheet.Cells[rowIndex, ActivityExportHeader.ClassName.ColumnIndex].Value = track.Class.Name;
                worksheet.Cells[rowIndex, ActivityExportHeader.TeacherName.ColumnIndex].Value = track.Class.Teacher.Name;
            }

            worksheet.AutoFixColumns(typeof(ActivityExportHeader));
        }

        public async Task<byte[]> ExportActivityForSchoolOnwer(Guid userId, Guid schoolId, Guid? classId)
        {
            IEnumerable<Entities.Class> efClasses = await this._unitOfWork.Classes.GetWithSchoolId(schoolId);
            List<Guid> classIds = efClasses.Where(c => classId.IsNullOrEmpty() || c.Id.Equals(classId.Value))
                                        .Select(c => c.Id)
                                        .ToList();

            IEnumerable<Entities.Track> efTracks = await this._unitOfWork.Tracks.GetFullInformationByIds(classIds);
            IEnumerable<Track> tracks = _mapper.Map<IEnumerable<Entities.Track>, IEnumerable<Track>>(efTracks);
            IEnumerable<Class> classes = tracks.Select(t => t.Class).DistinctBy(t => t.Id).ToList();

            var trackGroupByClass = tracks.GroupBy(t => t.ClassId).Select(x => new
            {
                ClassId = x.Key,
                Tracks = x.ToList()
            });

            byte[] result;
            using (var package = new ExcelPackage())
            {
                foreach (var groupOfClass in trackGroupByClass)
                {
                    Class classInfo = classes.SingleOrDefault(s => s.Id.Equals(groupOfClass.ClassId));
                    AppendSheetForEachClass(package.Workbook.Worksheets, classInfo, groupOfClass.Tracks);
                }                
                result = package.GetAsByteArray();
            }
            return result;
        }

        public async Task<byte[]> ExportActivityForTeacher(Guid teacherId, Guid classId)
        {
            IEnumerable<Entities.Track> efTracks = await _unitOfWork.Tracks.GetFullInformation(teacherId, classId);
            IEnumerable<Models.Track> tracks = _mapper.Map<IEnumerable<Entities.Track>, IEnumerable<Models.Track>>(efTracks);
            byte[] result;
            using (var package = new ExcelPackage())
            {              
                var worksheet = package.Workbook.Worksheets.Add("Children Tracking");
                worksheet.AppendHeader(typeof(ActivityExportHeader), 1);
                
                int rowIndex = 1;
                foreach (var track in tracks)
                {
                    rowIndex++;
                    worksheet.Cells[rowIndex, ActivityExportHeader.ChildName.ColumnIndex].Value = track.ChildName;
                    worksheet.Cells[rowIndex, ActivityExportHeader.TimeCheckIn.ColumnIndex].Value = track.TimeCheckIn.ToLocalTime();
                    worksheet.Cells[rowIndex, ActivityExportHeader.TimeCheckOut.ColumnIndex].Value = track.TimeCheckOut.ToLocalTime();
                    worksheet.Cells[rowIndex, ActivityExportHeader.ClassName.ColumnIndex].Value = track.Class.Name;
                    worksheet.Cells[rowIndex, ActivityExportHeader.TeacherName.ColumnIndex].Value = track.Class.Teacher.Name;
                }

                worksheet.AutoFixColumns(typeof(ActivityExportHeader));
                result = package.GetAsByteArray();
            }
            return result;
        }
    }
}
