﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210911205651_ChangePerson")]
    partial class ChangePerson
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Conversion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountInLari")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyFromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ExchangeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ReceivedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.HasIndex("PersonId");

                    b.ToTable("Conversions");
                });

            modelBuilder.Entity("Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEng")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Domain.Entities.ExchangeRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Buy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Sell")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId")
                        .IsUnique();

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecomendatorRecommendatorIdentityNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RecommendatorIdentityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityNumber")
                        .IsUnique()
                        .HasFilter("[IdentityNumber] IS NOT NULL");

                    b.HasIndex("RecomendatorRecommendatorIdentityNumber");

                    b.HasIndex("RecommendatorIdentityNumber");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Domain.Entities.Conversion", b =>
                {
                    b.HasOne("Domain.Entities.Currency", "CurrencyFrom")
                        .WithMany("ConversionsForFrom")
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Currency", "CurrencyTo")
                        .WithMany("ConversionsForTo")
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Person", "Person")
                        .WithMany("Conversions")
                        .HasForeignKey("PersonId");

                    b.Navigation("CurrencyFrom");

                    b.Navigation("CurrencyTo");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Entities.ExchangeRate", b =>
                {
                    b.HasOne("Domain.Entities.Currency", "Currency")
                        .WithOne("ExchangeRate")
                        .HasForeignKey("Domain.Entities.ExchangeRate", "CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.HasOne("Domain.Entities.Person", "Recomendator")
                        .WithMany("Recommendeds")
                        .HasForeignKey("RecomendatorRecommendatorIdentityNumber")
                        .HasPrincipalKey("RecommendatorIdentityNumber");

                    b.Navigation("Recomendator");
                });

            modelBuilder.Entity("Domain.Entities.Currency", b =>
                {
                    b.Navigation("ConversionsForFrom");

                    b.Navigation("ConversionsForTo");

                    b.Navigation("ExchangeRate");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Navigation("Conversions");

                    b.Navigation("Recommendeds");
                });
#pragma warning restore 612, 618
        }
    }
}
