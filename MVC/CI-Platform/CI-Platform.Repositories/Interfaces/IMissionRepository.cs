using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IMissionRepository
    {
        List<Mission> GetMissions();

        Mission GetMissionsById(int id);

        List<GoalMission> GetGoalMissions();

        GoalMission GetGoalMissionByMissionId(int id);

        List<Timesheet> GetTimeSheet();

        List<Timesheet> GetTimesheetByMissionId(int id);

        List<MissionSkill> GetMissionSkills();

        List<MissionSkill> GetMissionSkillsByMissionId(int id);

        List<MissionRating> GetMissionsRating();

        MissionRating GetMissionRatingByUserIdAndMissionId(int id, int mid);

        double GetSumOfMissionRatingByMissionId(int id);

        int GetTotalMissionRatingByMissionId(int id);

        List<MissionMedium> GetMissionMedia();

        List<MissionMedium> GetMissionMediaByMissionId(int id);

        List<MissionApplicatoin> GetMissionApplicatoinsByUserId(int id);

        MissionApplicatoin GetMissionApplicatoinByUserIdAndMissionId(int id, int mid);

        List<MissionApplicatoin> GetMissionApplicatoinsByMissionId(int mid);

        List<MissionApplicatoin> GetAllMissionApplicationSum();

        List<FavoriteMission> GetFavoriteMissionsByUserId(int id);

        FavoriteMission GetFavoriteMissionsByUserIdAndMissionId(int id, int mid);

        List<MissionDocument> GetFavoriteMissionDocumentsByMissionId(int mid);

        List<Comment> GetCommentsByMissionId(int mid);

        void LikeMission(FavoriteMission favoriteMission);

        void UnlikeMission(FavoriteMission favoriteMission);

        void Rating(MissionRating missionRating);

        void UpdateRating(MissionRating missionRating);

        void PostComment(Comment comment);

        void InserMissionInvitation(MissionInvite invite);

        void InserMissionApplication(MissionApplicatoin applicatoin);
    }
}
