using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.Repository.Entities;

namespace OnlineQuiz.Repository;

public partial class OnlineQuizSystemContext : DbContext
{
    public OnlineQuizSystemContext()
    {
    }

    public OnlineQuizSystemContext(DbContextOptions<OnlineQuizSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerAnswer> PlayerAnswers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionInRoom> QuestionInRooms { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C88E132FF7");

            entity.ToTable("Player");

            entity.HasIndex(e => e.RoomId, "IX_Players_RoomId");

            entity.HasIndex(e => e.TeamCode, "IX_Players_TeamCode");

            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nickname).HasMaxLength(50);

            entity.HasOne(d => d.Room).WithMany(p => p.Players)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__RoomId__4E88ABD4");
        });

        modelBuilder.Entity<PlayerAnswer>(entity =>
        {
            entity.HasKey(e => e.PlayerAnswerId).HasName("PK__PlayerAn__B300DBAC00DA2190");

            entity.ToTable("PlayerAnswer");

            entity.HasIndex(e => e.PlayerId, "IX_PlayerAnswers_PlayerId");

            entity.Property(e => e.AnsweredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SelectedAnswer)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerAnswers)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerAns__Playe__571DF1D5");

            entity.HasOne(d => d.QuestionInRoom).WithMany(p => p.PlayerAnswers)
                .HasForeignKey(d => d.QuestionInRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerAns__Quest__5812160E");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC4C18ED29");

            entity.ToTable("Question");

            entity.HasIndex(e => e.QuizId, "IX_Questions_QuizId");

            entity.Property(e => e.AnswerA).HasMaxLength(500);
            entity.Property(e => e.AnswerB).HasMaxLength(500);
            entity.Property(e => e.AnswerC).HasMaxLength(500);
            entity.Property(e => e.AnswerCorrect)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AnswerD).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.QuestionText).HasMaxLength(1000);
            entity.Property(e => e.TimeLimit).HasDefaultValue(10);

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__QuizId__440B1D61");
        });

        modelBuilder.Entity<QuestionInRoom>(entity =>
        {
            entity.HasKey(e => e.QuestionInRoomId).HasName("PK__Question__9F768C9724403E72");

            entity.ToTable("QuestionInRoom");

            entity.HasIndex(e => e.RoomId, "IX_QuestionInRoom_RoomId");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionInRooms)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuestionI__Quest__5165187F");

            entity.HasOne(d => d.Room).WithMany(p => p.QuestionInRooms)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuestionI__RoomI__52593CB8");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quiz__8B42AE8E2C5FC6F9");

            entity.ToTable("Quiz");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quiz__UserId__3E52440B");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863939A5F0F330");

            entity.ToTable("Room");

            entity.HasIndex(e => e.HostId, "IX_Rooms_HostId");

            entity.HasIndex(e => e.RoomCode, "UQ__Room__4F9D5231543D2A70").IsUnique();

            entity.Property(e => e.EndedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoomCode).HasMaxLength(10);
            entity.Property(e => e.StartedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Host).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__HostId__49C3F6B7");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__QuizId__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C0CC14FE4");

            entity.HasIndex(e => e.FullName, "UQ__Users__89C60F11EFC7ED15").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341D73E220").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
