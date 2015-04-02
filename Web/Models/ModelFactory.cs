using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using DRS2Data;
using DRS2Data.Models;

namespace DRS2Web.Models
{
    public class ModelFactory
    {
        private System.Web.Http.Routing.UrlHelper _UrlHelper;
        private IDRSRepository _repo;

        public ModelFactory(HttpRequestMessage request, IDRSRepository repo)
        {
            _UrlHelper = new System.Web.Http.Routing.UrlHelper(request);
            _repo = repo;
        }

        public SystemModel Create(DRSSystem system)
        {
            return new SystemModel()
                   {
                       Id = system.DRSSystemID,
                       Name = system.Name,
                       Description = system.Description,
                       Url = _UrlHelper.Link("Systems", new { id = system.DRSSystemID }),
                   };
        }

        public DRSSystem Parse(SystemModel model)
        {
            try
            {
                var system = new DRSSystem()
                             {
                                 Description = model.Description,
                                 DRSSystemID = model.Id,
                                 Name = model.Name,
                                 ReviewEntities = _repo.GetAllReviewsEntitiesForSystem(model.Id).ToList()
                             };
                return system;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public IssueModel Create(Issue issue)
        {
            return new IssueModel()
                   {
                       Id = issue.IssueID,
                       IssueIdentity = issue.IssueIdentity,
                       ReviewEntity = Create(issue.ReviewEntity),
                       IssueUrl = issue.URL,
                       Url = _UrlHelper.Link("Issues", new { id = issue.IssueID })
                   };
        }

        public List<IssueModel> Create(ICollection<Issue> issues)
        {
            var list = issues.ToList().Select(
                x => new IssueModel
                     {
                         Id = x.IssueID,
                         IssueIdentity = x.IssueIdentity,
                         ReviewEntity = Create(x.ReviewEntity),
                         IssueUrl = x.URL,
                         Url = _UrlHelper.Link("Issues", new { id = x.IssueID })
                     }
                ).ToList();

            return list;
        }

        public ReviewEntityModel Create(ReviewEntity entity)
        {
            return new ReviewEntityModel()
                   {
                       Id = entity.ReviewID,
                       System = Create(entity.System),
                       Description = entity.Description,
                       PermanentUrl = entity.URL,
                       IdentityString = entity.IdentityString,
                       Issues = Create(entity.Issues),
                       Url = _UrlHelper.Link("Entities", new { id = entity.ReviewID })
                   };
        }

        public List<ReviewEntityModel> Create(ICollection<ReviewEntity> reviewEntities)
        {
            var list = reviewEntities.ToList().Select(
                x => new ReviewEntityModel
                {
                    Id = x.ReviewID,
                    System = Create(x.System),
                    Description = x.Description,
                    PermanentUrl = x.URL,
                    IdentityString = x.IdentityString,
                    Issues = Create(x.Issues),
                    Url = _UrlHelper.Link("Entities", new { id = x.ReviewID })
                }
                ).ToList();

            return list;
        }

        public LogModel Create(Log log)
        {
            return new LogModel()
                   {
                       Id = log.LogID,
                       Message = log.Message,
                       Severity = log.Severity,
                       User = log.User,
                       Url = _UrlHelper.Link("Logs", new { id = log.LogID })
                   };
        }

        public List<LogModel> Create(ICollection<Log> logs)
        {
            var list = logs.ToList().Select(
                x => new LogModel
                {
                    Id = x.LogID,
                    User = x.User,
                    Message = x.Message,
                    Severity = x.Severity,
                    Url = _UrlHelper.Link("Issues", new { id = x.LogID })
                }
                ).ToList();

            return list;
        }

        public UserBaseModel CreateBasic(User user)
        {
            return new UserBaseModel
                   {
                       Id = user.UserId,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       RegistrationDate = user.RegistrationDate,
                       UserName = user.UserName,
                       Url = _UrlHelper.Link("Users", new {id = user.UserId})
                   };
        }

        public UserDetailModel Create(User user)
        {
            return new UserDetailModel()
                   {
                       Id = user.UserId,
                       Email = user.Email,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Fullname = user.FirstName + " " + user.LastName,
                       LastLoginDate = user.LastLoginDate,
                       RegistrationDate = user.RegistrationDate,
                       DateOfBirth = user.DateOfBirth,
                       UserName = user.UserName,
                       Logs = Create(user.Logs),
                       ReviewEntities = Create(user.ReviewEntities),
                       Url = _UrlHelper.Link("Users", new {id = user.UserId})
                   };
        }

    }
}