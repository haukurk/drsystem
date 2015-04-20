using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using API.Security;
using Data;
using Data.Models;

namespace Api.Models
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

            if (system == null)
                return null;

            return new SystemModel()
                   {
                       Id = system.Id,
                       Name = system.Name,
                       Description = system.Description,
                       ReviewsCount = _repo.GetAllReviewsEntitiesForSystem(system.Id).Count(),
                       Url = _UrlHelper.Link("Systems", new { id = system.Id }),
                   };
        }

        public DRSSystem Parse(SystemModel model)
        {
            try
            {
                var system = new DRSSystem()
                             {
                                 Description = model.Description,
                                 Id = model.Id,
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
            if (issue == null)
                return null;

            return new IssueModel()
                   {
                       Id = issue.Id,
                       IssueIdentity = issue.IssueIdentity,
                       ReviewEntity = CreateBasic(issue.ReviewEntity),
                       IssueUrl = issue.URL,
                       Url = _UrlHelper.Link("Issues", new { id = issue.Id })
                   };
        }

        public List<IssueModel> Create(ICollection<Issue> issues)
        {

            if (issues == null)
                return null;

            var list = issues.ToList().Select(
                x => new IssueModel
                     {
                         Id = x.Id,
                         IssueIdentity = x.IssueIdentity,
                         ReviewEntity = CreateBasic(x.ReviewEntity),
                         IssueUrl = x.URL,
                         Url = _UrlHelper.Link("Issues", new { id = x.Id })
                     }
                ).ToList();

            return list;
        }

        public ReviewEntityModel Create(ReviewEntity entity)
        {
            if (entity == null)
                return null;

            return new ReviewEntityModel()
                   {
                       Id = entity.Id,
                       System = Create(entity.System),
                       Description = entity.Description,
                       PermanentUrl = entity.URL,
                       IdentityString = entity.IdentityString,
                       Issues = Create(entity.Issues),
                       User = CreateBasic(entity.User),
                       LastNotified = entity.LastNotified,
                       NotificationPeriod = entity.NotificationPeriod,
                       Responsible = entity.Responsible,
                       ResponsibleEmail = entity.ResponsibleEmail,
                       Url = _UrlHelper.Link("ReviewEntities", new { id = entity.Id })
                   };
        }

        public ReviewEntityBasicModel CreateBasic(ReviewEntity entity)
        {
            if (entity == null)
                return null;

            return new ReviewEntityBasicModel()
            {
                Id = entity.Id,
                Description = entity.Description,
                PermanentUrl = entity.URL,
                IdentityString = entity.IdentityString,
                LastNotified = entity.LastNotified,
                NotificationPeriod = entity.NotificationPeriod,
                Responsible = entity.Responsible,
                ResponsibleEmail = entity.ResponsibleEmail,
                Url = _UrlHelper.Link("ReviewEntities", new { id = entity.Id })
            };
        }

        public ReviewEntity Parse(ReviewEntityModel entity)
        {
            if (entity == null)
                return null;

            var review = new ReviewEntity()
            {
                Id = entity.Id,
                Description = entity.Description,
                IdentityString = entity.IdentityString,
                LastNotified = entity.LastNotified,
                NotificationPeriod = entity.NotificationPeriod,
                Responsible = entity.Responsible,
                ResponsibleEmail = entity.ResponsibleEmail,
                URL = entity.Url
            };

            // Populate navigation attributes.
            if (entity.System.Id > 0) review.System = _repo.GetSystem(entity.System.Id);
            if (entity.User.Id > 0) review.User = _repo.GetUser(entity.User.Id);
            if (entity.Id > 0 ) review.Issues = _repo.GetAllIssuesForReview(entity.Id).ToList();

            return review;

        }

        public List<ReviewEntityModel> Create(ICollection<ReviewEntity> reviewEntities)
        {
            if (reviewEntities == null)
                return null;

            var list = reviewEntities.ToList().Select(
                x => new ReviewEntityModel
                {
                    Id = x.Id,
                    System = Create(x.System),
                    Description = x.Description,
                    PermanentUrl = x.URL,
                    IdentityString = x.IdentityString,
                    Issues = Create(x.Issues),
                    Url = _UrlHelper.Link("ReviewEntities", new { id = x.Id })
                }
                ).ToList();

            return list;
        }

        public LogModel Create(Log log)
        {
            if (log == null)
                return null;

            return new LogModel()
                   {
                       Id = log.Id,
                       Message = log.Message,
                       Severity = log.Severity,
                       User = CreateBasic(log.User),
                       Created = log.Created,
                       Url = _UrlHelper.Link("Logs", new { logId = log.Id })
                   };
        }

        public List<LogModel> Create(ICollection<Log> logs)
        {
            if (logs == null)
                return null;

            var list = logs.ToList().Select(
                x => new LogModel
                {
                    Id = x.Id,
                    User = CreateBasic(x.User),
                    Message = x.Message,
                    Created = x.Created,
                    Severity = x.Severity,
                    Url = _UrlHelper.Link("Logs", new { logId = x.Id })
                }
                ).ToList();

            return list;
        }

        public Log Parse(LogModel log)
        {
            if (log == null)
                return null;

            var entity = new Log()
            {
                Id = log.Id,
                Message = log.Message,
                Created = log.Created,
                Severity = log.Severity
            };

            // Populate navigation attributes.
            if (log.User.Id > 0) entity.User = _repo.GetUser(entity.User.Id);

            return entity;

        }

        public UserBaseModel CreateBasic(User user)
        {
            if (user == null)
                return null;

            return new UserBaseModel
                   {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       RegistrationDate = user.RegistrationDate,
                       UserName = user.UserName,
                       Url = _UrlHelper.Link("Users", new {userName = user.UserName})
                   };
        }

        public UserDetailModel Create(User user)
        {
            if (user == null)
                return null;

            var userModel = new UserDetailModel()
                   {
                       Id = user.Id,
                       Email = user.Email,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       LastLoginDate = user.LastLoginDate,
                       RegistrationDate = user.RegistrationDate,
                       DateOfBirth = user.DateOfBirth,
                       UserName = user.UserName,
                       Logs = Create(user.Logs),
                       ReviewEntities = Create(user.ReviewEntities),
                       Url = _UrlHelper.Link("Users", new {userName = user.UserName})
                   };

            return userModel;
        }

    }
}