using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data;
using NLog;

namespace Data
{
    public class DRSRepository : IDRSRepository
    {

        private DRSContext _ctx;

        public DRSRepository(DRSContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<DRSSystem> GetAllSystems()
        {
            DRSLogger.Instance.Debug("Getting all systems...");
            return _ctx.Systems.AsQueryable();
        }

        public DRSSystem GetSystem(int systemid)
        {
            return _ctx.Systems.Find(systemid);
        }

        public bool SystemExists(string systemname)
        {
            return _ctx.Systems.Any(c => c.Name == systemname);
        }

        public bool DeleteSystem(int systemid)
        {
            try
            {
                var entity = _ctx.Systems.Find(systemid);
                if (entity != null)
                {
                    _ctx.Systems.Remove(entity);
                    return true;
                }
            }
            catch(Exception ex)
            {
                DRSLogger.Instance.Error("Deleting System Error: " + ex.Message);
            }

            return false;
        }

        public bool Insert(DRSSystem system)
        {
            try
            {
                _ctx.Systems.Add(system);
                return true;
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Error("Inserting System Error: " + ex.Message);
            }

            return false;
        }

        public bool Update(DRSSystem orginalsystem, DRSSystem updatedSystem)
        {
            _ctx.Entry(orginalsystem).CurrentValues.SetValues(updatedSystem);
            return true;
        }

        public IQueryable<Issue> GetAllIssues()
        {
            return _ctx.Issues.AsQueryable();
        }

        public Issue GetIssue(int issueid)
        {
            return _ctx.Issues.Find(issueid);
        }

        public bool DeleteIssue(int issueid)
        {
            try
            {
                var entity = _ctx.Issues.Find(issueid);
                if (entity != null)
                {
                    _ctx.Issues.Remove(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Error("Deleting Issue Error: " + ex.Message);
            }

            return false;
        }

        public bool Insert(Issue issue)
        {
            try
            {
                _ctx.Issues.Add(issue);
                return true;
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Error("Insert Issue Error: " + ex.Message);
            }

            return false;
        }

        public bool Update(Issue orginalIssue, Issue updatedIssue)
        {
            _ctx.Entry(orginalIssue).CurrentValues.SetValues(updatedIssue);

            return true;
        }

        public IQueryable<Log> GetAllLogs()
        {
            return _ctx.Logs.AsQueryable();
        }

        public Log GetLog(int logid)
        {
            return _ctx.Logs.Find(logid);
        }

        public bool DeleteLog(int logid)
        {
            try
            {
                var entity = _ctx.Logs.Find(logid);
                if (entity != null)
                {
                    _ctx.Logs.Remove(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Deleting Log Error: " + ex.Message);
            }

            return false;
        }

        public bool Insert(Log log)
        {
            try
            {
                _ctx.Logs.Add(log);
                return true;
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Insert Log Error: " + ex.Message);
            }

            return false;
        }

        public bool Update(Log orginaLog, Log updatedLog)
        {
            _ctx.Entry(orginaLog).CurrentValues.SetValues(updatedLog);

            return true;
        }

        public IQueryable<ReviewEntity> GetAllReviewsEntities()
        {
            return _ctx.ReviewEntities.AsQueryable();
        }

        public IQueryable<ReviewEntity> GetAllReviewsEntitiesForSystem(int systemid)
        {
            return _ctx.ReviewEntities.Where(r => r.System.DRSSystemID == systemid);
        }

        public ReviewEntity GetReviewEntity(int reviewentityid)
        {
            return _ctx.ReviewEntities.Find(reviewentityid);
        }

        public bool DeleteReviewEntity(int reviewentityid)
        {
            try
            {
                var entity = _ctx.ReviewEntities.Find(reviewentityid);
                if (entity != null)
                {
                    _ctx.ReviewEntities.Remove(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Deleting Review Error: " + ex.Message);
            }

            return false;
        }

        public bool Insert(ReviewEntity reviewEntity)
        {
            try
            {
                _ctx.ReviewEntities.Add(reviewEntity);
                DRSLogger.Instance.Info("Added Review " + reviewEntity);
                return true;
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Insert Review Error: " + ex.Message);
            }

            return false;
        }

        public bool Update(ReviewEntity orginaReviewEntity, ReviewEntity updatedReviewEntity)
        {
            _ctx.Entry(orginaReviewEntity).CurrentValues.SetValues(updatedReviewEntity);
            orginaReviewEntity.Issues = updatedReviewEntity.Issues;
            orginaReviewEntity.System = updatedReviewEntity.System;

            return true;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _ctx.Users.AsQueryable();
        }

        public User GetUser(int userid)
        {
            return _ctx.Users.Find(userid);
        }

        public User GetUser(string userName)
        {
            return _ctx.Users.Where(s => s.UserName == userName).SingleOrDefault();
        }

        public bool DeleteUser(int userid)
        {
            try
            {
                var entity = _ctx.Users.Find(userid);
                if (entity != null)
                {
                    _ctx.Users.Remove(entity);
                    DRSLogger.Instance.Info("Deleted user " + entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Deleting User Error: " + ex.Message);
            }

            return false;
        }

        public bool Insert(User user)
        {
            try
            {
                _ctx.Users.Add(user);
                DRSLogger.Instance.Info("Added user " + user);
                return true;
            }
            catch (Exception ex)
            {
                DRSLogger.Instance.Debug("Insert User Error: " + ex.Message);
            }

            return false;
        }

        public bool Update(User originalUser, User updatedUser)
        {
            _ctx.Entry(originalUser).CurrentValues.SetValues(updatedUser);

            return true;
        }

        public bool LoginUser(string userName, string password)
        {
            var user = _ctx.Users.Where(s => s.UserName == userName).SingleOrDefault();

            if (user != null)
            {
                var verify = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (verify)
                {
                    return true;
                }
            }

            return false;
        }


        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
