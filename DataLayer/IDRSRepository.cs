using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public interface IDRSRepository
    {
        IQueryable<DRSSystem> GetAllSystems();
        DRSSystem GetSystem(int systemid);
        bool SystemExists(string systemname);
        bool DeleteSystem(int systemid);
        bool Insert(DRSSystem system);
        bool Update(DRSSystem orginalsystem, DRSSystem updatedSystem);

        IQueryable<Issue> GetAllIssues();
        Issue GetIssue(int issueid);
        bool DeleteIssue(int issueid);
        bool Insert(Issue issue);
        bool Update(Issue orginalIssue, Issue updatedIssue);

        IQueryable<Log> GetAllLogs();
        Log GetLog(int logid);
        bool DeleteLog(int logid);
        bool Insert(Log log);
        bool Update(Log orginaLog, Log updatedLog);

        IQueryable<ReviewEntity> GetAllReviewsEntities();
        IQueryable<ReviewEntity> GetAllReviewsEntitiesForSystem(int systemid);
        ReviewEntity GetReviewEntity(int reviewentityid);
        bool DeleteReviewEntity(int reviewentityid);
        bool Insert(ReviewEntity reviewEntity);
        bool Update(ReviewEntity orginaReviewEntity, ReviewEntity updatedReviewEntity);

        IQueryable<User> GetAllUsers();
        User GetUser(int userid);
        bool DeleteUser(int userid);
        User GetUser(string userName);
        bool Insert(User user);
        bool Update(User originalUser, User updatedUser);
        bool LoginUser(string userName, string password);

        bool SaveAll();

    }
}
