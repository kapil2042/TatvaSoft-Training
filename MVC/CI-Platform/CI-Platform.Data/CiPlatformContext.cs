using System;
using System.Collections.Generic;
using CI_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Data;

public partial class CiPlatformContext : DbContext
{
    public CiPlatformContext()
    {
    }

    public CiPlatformContext(DbContextOptions<CiPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CmsPage> CmsPages { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FavoriteMission> FavoriteMissions { get; set; }

    public virtual DbSet<GoalMission> GoalMissions { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<MissionApplicatoin> MissionApplicatoins { get; set; }

    public virtual DbSet<MissionDocument> MissionDocuments { get; set; }

    public virtual DbSet<MissionInvite> MissionInvites { get; set; }

    public virtual DbSet<MissionMedium> MissionMedia { get; set; }

    public virtual DbSet<MissionRating> MissionRatings { get; set; }

    public virtual DbSet<MissionSkill> MissionSkills { get; set; }

    public virtual DbSet<MissionTheme> MissionThemes { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryInvite> StoryInvites { get; set; }

    public virtual DbSet<StoryMedium> StoryMedia { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=CI-Platform; user id=sa; password=123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__43AA4141D396DC5C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PK__banner__10373C341AC474CF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SortOrder).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city__031491A85C6EEDB1");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__city__country_id__1C873BEC");
        });

        modelBuilder.Entity<CmsPage>(entity =>
        {
            entity.HasKey(e => e.CsmPageId).HasName("PK__cms_page__44CC3BCBC27FB954");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__comment__E7957687F236B288");

            entity.Property(e => e.ApprovalStatus).HasDefaultValueSql("('PENDING')");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__mission__52E34C9D");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__user_id__51EF2864");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__country__7E8CD055605D1A23");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<FavoriteMission>(entity =>
        {
            entity.HasKey(e => e.FavouriteMissionId).HasName("PK__favorite__94E4D8CAD44FBBA8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.FavoriteMissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorite___missi__5A846E65");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteMissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorite___user___59904A2C");
        });

        modelBuilder.Entity<GoalMission>(entity =>
        {
            entity.HasKey(e => e.GoalMissionId).HasName("PK__goal_mis__358E02C75DE3B800");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.GoalMissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__goal_miss__missi__603D47BB");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.MissionId).HasName("PK__mission__B5419AB2B0F1C163");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.City).WithMany(p => p.Missions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission__city_id__41B8C09B");

            entity.HasOne(d => d.Country).WithMany(p => p.Missions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission__country__42ACE4D4");

            entity.HasOne(d => d.Theme).WithMany(p => p.Missions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission__theme_i__40C49C62");
        });

        modelBuilder.Entity<MissionApplicatoin>(entity =>
        {
            entity.HasKey(e => e.MissionApplicationId).HasName("PK__mission___DF92838A809146CF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionApplicatoins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_a__missi__67DE6983");

            entity.HasOne(d => d.User).WithMany(p => p.MissionApplicatoins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_a__user___66EA454A");
        });

        modelBuilder.Entity<MissionDocument>(entity =>
        {
            entity.HasKey(e => e.MissionDocumentId).HasName("PK__mission___E80E0CC8A78482DD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_d__missi__6D9742D9");
        });

        modelBuilder.Entity<MissionInvite>(entity =>
        {
            entity.HasKey(e => e.MissionInviteId).HasName("PK__mission___A97ED158BA1BD986");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FromUser).WithMany(p => p.MissionInviteFromUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_i__from___73501C2F");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionInvites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_i__missi__753864A1");

            entity.HasOne(d => d.ToUser).WithMany(p => p.MissionInviteToUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_i__to_us__74444068");
        });

        modelBuilder.Entity<MissionMedium>(entity =>
        {
            entity.HasKey(e => e.MissionMediaId).HasName("PK__mission___848A78E865CB8954");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MediaDefault).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_m__missi__7CD98669");
        });

        modelBuilder.Entity<MissionRating>(entity =>
        {
            entity.HasKey(e => e.MissionRatingId).HasName("PK__mission___320E5172235F45E7");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionRatings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_r__missi__047AA831");

            entity.HasOne(d => d.User).WithMany(p => p.MissionRatings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_r__user___038683F8");
        });

        modelBuilder.Entity<MissionSkill>(entity =>
        {
            entity.HasKey(e => e.MissionSkillId).HasName("PK__mission___82712008E8ADA4DF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_s__missi__4CF5691D");

            entity.HasOne(d => d.Skill).WithMany(p => p.MissionSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_s__skill__4C0144E4");
        });

        modelBuilder.Entity<MissionTheme>(entity =>
        {
            entity.HasKey(e => e.MissionThemeId).HasName("PK__mission___4925C5ACAB231112");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__skill__FBBA8379DF4A0FA3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__story__66339C56E50F7247");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("('DRAFT')");

            entity.HasOne(d => d.Mission).WithMany(p => p.Stories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story__mission_i__220B0B18");

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story__user_id__2116E6DF");
        });

        modelBuilder.Entity<StoryInvite>(entity =>
        {
            entity.HasKey(e => e.StoryInviteId).HasName("PK__story_in__044978678FFE745F");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FromUser).WithMany(p => p.StoryInviteFromUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story_inv__from___27C3E46E");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryInvites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story_inv__story__29AC2CE0");

            entity.HasOne(d => d.ToUser).WithMany(p => p.StoryInviteToUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story_inv__to_us__28B808A7");
        });

        modelBuilder.Entity<StoryMedium>(entity =>
        {
            entity.HasKey(e => e.StoryMediaId).HasName("PK__story_me__29BD053CEDFDD8AD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story_med__story__2F650636");
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.TimesheetId).HasName("PK__timeshee__7BBF506820EDF912");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("('SUBMIT_FOR_APPROVAL')");

            entity.HasOne(d => d.Mission).WithMany(p => p.Timesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__timesheet__missi__37FA4C37");

            entity.HasOne(d => d.User).WithMany(p => p.Timesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__timesheet__user___370627FE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FCD73FD03");

            entity.Property(e => e.CityId).HasDefaultValueSql("((1))");
            entity.Property(e => e.CountryId).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__city_id__4959E263");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__country_i__4A4E069C");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.UserSkillId).HasName("PK__user_ski__FD3B576B9A4E246A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_skil__skill__567ED357");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_skil__user___558AAF1E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
