using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<FilmModel> Films { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<CountryFilmModel> FilmCountries { get; set; }
        public DbSet<FilmGenersModel> FilmGeneres { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FilmModel>()
                .HasMany(x => x.Genre)
                .WithOne(t => t.Film)
                .HasForeignKey(x => x.FilmModelId)
                .HasPrincipalKey(x => x.FilmGenereModelId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<FilmModel>()
                .HasMany(x => x.Countries)
                .WithOne(x => x.Film)
                .HasForeignKey(x => x.FilmModelId)
                .HasPrincipalKey(x => x.CountryFilmModelId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<IdentityRole>().HasData(
              new IdentityRole
              {
                  Id = "A43B9C3A-BCBC-4600-A726-15C9435477AF",
                  Name = "User",
                  NormalizedName = "USER"
              },
              new IdentityRole
              {
                  Id = "B3616F56-CD53-464D-8E6F-E49ADF2F3FB5",
                  Name = "Editor",
                  NormalizedName = "EDITOR"
              },
              new IdentityRole
              {
                  Id = "E384D4E7-7B9A-4B31-9A94-CC566E344E5B",
                  Name = "Moderator",
                  NormalizedName = "MODERATOR"
              },
              new IdentityRole
              {
                  Id = "C553670B-3A92-4D8D-B841-D5850EE19B9B",
                  Name = "Admin",
                  NormalizedName = "ADMIN"
              });
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "DFAEB25F-EAA8-49F1-B18F-E3F13DAFD3C0",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "SUp3RAdmin_2021")

                });
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "C553670B-3A92-4D8D-B841-D5850EE19B9B", //Admin
                UserId = "DFAEB25F-EAA8-49F1-B18F-E3F13DAFD3C0"
            },
            new IdentityUserRole<string>
            {
                RoleId = "E384D4E7-7B9A-4B31-9A94-CC566E344E5B", //Moderator
                UserId = "DFAEB25F-EAA8-49F1-B18F-E3F13DAFD3C0"
            },
            new IdentityUserRole<string>
            {
                RoleId = "B3616F56-CD53-464D-8E6F-E49ADF2F3FB5", //Editor
                UserId = "DFAEB25F-EAA8-49F1-B18F-E3F13DAFD3C0"
            },
            new IdentityUserRole<string>
            {
                RoleId = "A43B9C3A-BCBC-4600-A726-15C9435477AF", //User
                UserId = "DFAEB25F-EAA8-49F1-B18F-E3F13DAFD3C0"
            });
            builder.Entity<GenreModel>().HasIndex(p => p.Genre).IsUnique(true);
            builder.Entity<CountryModel>().HasIndex(c => c.Name).IsUnique(true);
            builder.Entity<FilmModel>().HasIndex(x => x.Name).IsUnique(true);
            builder.Entity<GenreModel>().HasData(
               new GenreModel
               {
                   Id = 1,
                   Genre = "Anime"
               },
               new GenreModel
               {
                   Id = 2,
                   Genre = "Action"
               },
               new GenreModel
               {
                   Id = 3,
                   Genre = "Adult"
               },
               new GenreModel
               {
                   Id = 4,
                   Genre = "Adventure"
               },
               new GenreModel
               {
                   Id = 5,
                   Genre = "Animation"
               },
               new GenreModel
               {
                   Id = 6,
                   Genre = "Biography"
               },
               new GenreModel
               {
                   Id = 7,
                   Genre = "Comedy"
               },
               new GenreModel
               {
                   Id = 9,
                   Genre = "Documentary"
               },
               new GenreModel
               {
                   Id = 10,
                   Genre = "Drama"
               },
               new GenreModel
               {
                   Id = 11,
                   Genre = "Family"
               },
               new GenreModel
               {
                   Id = 12,
                   Genre = "Fantasy"
               },
               new GenreModel
               {
                   Id = 13,
                   Genre = "History"
               },
               new GenreModel
               {
                   Id = 14,
                   Genre = "Horror"
               },
               new GenreModel
               {
                   Id = 15,
                   Genre = "Musical"
               },
               new GenreModel
               {
                   Id = 16,
                   Genre = "Music"
               },
               new GenreModel
               {
                   Id = 17,
                   Genre = "Mystery"
               },
               new GenreModel
               {
                   Id = 18,
                   Genre = "Romance"
               },
               new GenreModel
               {
                   Id = 19,
                   Genre = "Sci-Fi"
               },
               new GenreModel
               {
                   Id = 20,
                   Genre = "Short"
               },
               new GenreModel
               {
                   Id = 21,
                   Genre = "Sport"
               },
               new GenreModel
               {
                   Id = 22,
                   Genre = "Thriller"
               },
               new GenreModel
               {
                   Id = 23,
                   Genre = "War"
               },
               new GenreModel
               {
                   Id = 24,
                   Genre = "Western"
               }
               );

            builder.Entity<CountryModel>().HasData(
                new CountryModel
                {
                    Id = 1,
                    Name = "Afghanistan"
                },
                new CountryModel
                {
                    Id = 2,
                    Name = "Albania"
                },
                new CountryModel
                {
                    Id = 3,
                    Name = "Algeria"
                },
                new CountryModel
                {
                    Id = 4,
                    Name = "Andorra"
                },
                new CountryModel
                {
                    Id = 5,
                    Name = "Angola"
                },
                new CountryModel
                {
                    Id = 6,
                    Name = "Antigua and Barbuda"
                },
                new CountryModel
                {
                    Id = 7,
                    Name = "Argentina"
                },
                new CountryModel
                {
                    Id = 8,
                    Name = "Armenia"
                },
                new CountryModel
                {
                    Id = 9,
                    Name = "Australia"
                },
                new CountryModel
                {
                    Id = 10,
                    Name = "Austria"
                },
                new CountryModel
                {
                    Id = 11,
                    Name = "Azerbaijan"
                },
                new CountryModel
                {
                    Id = 12,
                    Name = "Bahamas"
                },
                new CountryModel
                {
                    Id = 13,
                    Name = "Bahrain"
                },
                new CountryModel
                {
                    Id = 14,
                    Name = "Bangladesh"
                },
                new CountryModel
                {
                    Id = 15,
                    Name = "Barbados"
                },
                new CountryModel
                {
                    Id = 16,
                    Name = "Belarus"
                },
                new CountryModel
                {
                    Id = 17,
                    Name = "Belgium"
                },
                new CountryModel
                {
                    Id = 18,
                    Name = "Belize"
                },
                new CountryModel
                {
                    Id = 19,
                    Name = "Benin"
                },
                new CountryModel
                {
                    Id = 20,
                    Name = "Bhutan"
                },
                new CountryModel
                {
                    Id = 21,
                    Name = "Bolivia"
                },
                new CountryModel
                {
                    Id = 22,
                    Name = "Bosnia and Herzegovina"
                },
                new CountryModel
                {
                    Id = 23,
                    Name = "Botswana"
                },
                new CountryModel
                {
                    Id = 24,
                    Name = "Brazil"
                },
                new CountryModel
                {
                    Id = 25,
                    Name = "Brunei"
                },
                new CountryModel
                {
                    Id = 26,
                    Name = "Bulgaria"
                },
                new CountryModel
                {
                    Id = 27,
                    Name = "Burkina Faso"
                },
                new CountryModel
                {
                    Id = 28,
                    Name = "Burundi"
                },
                new CountryModel
                {
                    Id = 29,
                    Name = "Cabo Verde"
                },
                new CountryModel
                {
                    Id = 30,
                    Name = "Cambodia"
                },
                new CountryModel
                {
                    Id = 31,
                    Name = "Cameroon"
                },
                new CountryModel
                {
                    Id = 32,
                    Name = "Canada"
                },
                new CountryModel
                {
                    Id = 33,
                    Name = "Central African Republic"
                },
                new CountryModel
                {
                    Id = 34,
                    Name = "Chad"
                },
                new CountryModel
                {
                    Id = 35,
                    Name = "Chile"
                },
                new CountryModel
                {
                    Id = 36,
                    Name = "China"
                },
                new CountryModel
                {
                    Id = 37,
                    Name = "Colombia"
                },
                new CountryModel
                {
                    Id = 38,
                    Name = "Comoros"
                },
                new CountryModel
                {
                    Id = 39,
                    Name = "Congo"
                },
                new CountryModel
                {
                    Id = 40,
                    Name = "Costa Rica"
                },
                new CountryModel
                {
                    Id = 41,
                    Name = "Croatia"
                },
                new CountryModel
                {
                    Id = 42,
                    Name = "Cuba"
                },
                new CountryModel
                {
                    Id = 43,
                    Name = "Cyprus"
                },
                new CountryModel
                {
                    Id = 44,
                    Name = "Czechia"
                },
                new CountryModel
                {
                    Id = 45,
                    Name = "Côte d'Ivoire"
                },
                new CountryModel
                {
                    Id = 46,
                    Name = "Denmark"
                },
                new CountryModel
                {
                    Id = 47,
                    Name = "Djibouti"
                },
                new CountryModel
                {
                    Id = 48,
                    Name = "Dominica"
                },
                new CountryModel
                {
                    Id = 49,
                    Name = "Dominican Republic"
                },
                new CountryModel
                {
                    Id = 50,
                    Name = "DR Congo"
                },
                new CountryModel
                {
                    Id = 51,
                    Name = "Ecuador"
                },
                new CountryModel
                {
                    Id = 52,
                    Name = "Egypt"
                },
                new CountryModel
                {
                    Id = 53,
                    Name = "El Salvador"
                },
                new CountryModel
                {
                    Id = 54,
                    Name = "Equatorial Guinea"
                },
                new CountryModel
                {
                    Id = 55,
                    Name = "Eritrea"
                },
                new CountryModel
                {
                    Id = 56,
                    Name = "Estonia"
                },
                new CountryModel
                {
                    Id = 57,
                    Name = "Eswatini"
                },
                new CountryModel
                {
                    Id = 58,
                    Name = "Ethiopia"
                },
                new CountryModel
                {
                    Id = 59,
                    Name = "Fiji"
                },
                new CountryModel
                {
                    Id = 60,
                    Name = "Finland"
                },
                new CountryModel
                {
                    Id = 61,
                    Name = "France"
                },
                new CountryModel
                {
                    Id = 62,
                    Name = "Gabon"
                },
                new CountryModel
                {
                    Id = 63,
                    Name = "Gambia"
                },
                new CountryModel
                {
                    Id = 64,
                    Name = "Georgia"
                },
                new CountryModel
                {
                    Id = 65,
                    Name = "Germany"
                },
                new CountryModel
                {
                    Id = 66,
                    Name = "Ghana"
                },
                new CountryModel
                {
                    Id = 67,
                    Name = "Greece"
                },
                new CountryModel
                {
                    Id = 68,
                    Name = "Grenada"
                },
                new CountryModel
                {
                    Id = 69,
                    Name = "Guatemala"
                },
                new CountryModel
                {
                    Id = 70,
                    Name = "Guinea"
                },
                new CountryModel
                {
                    Id = 71,
                    Name = "Guinea-Bissau"
                },
                new CountryModel
                {
                    Id = 72,
                    Name = "Guyana"
                },
                new CountryModel
                {
                    Id = 73,
                    Name = "Haiti"
                },
                new CountryModel
                {
                    Id = 74,
                    Name = "Holy See"
                },
                new CountryModel
                {
                    Id = 75,
                    Name = "Honduras"
                },
                new CountryModel
                {
                    Id = 76,
                    Name = "Hungary"
                },
                new CountryModel
                {
                    Id = 77,
                    Name = "Iceland"
                },
                new CountryModel
                {
                    Id = 78,
                    Name = "India"
                },
                new CountryModel
                {
                    Id = 79,
                    Name = "Indonesia"
                },
                new CountryModel
                {
                    Id = 80,
                    Name = "Iran"
                },
                new CountryModel
                {
                    Id = 81,
                    Name = "Iraq"
                },
                new CountryModel
                {
                    Id = 82,
                    Name = "Ireland"
                },
                new CountryModel
                {
                    Id = 83,
                    Name = "Israel"
                },
                new CountryModel
                {
                    Id = 84,
                    Name = "Italy"
                },
                new CountryModel
                {
                    Id = 85,
                    Name = "Jamaica"
                },
                new CountryModel
                {
                    Id = 86,
                    Name = "Japan"
                },
                new CountryModel
                {
                    Id = 87,
                    Name = "Jordan"
                },
                new CountryModel
                {
                    Id = 88,
                    Name = "Kazakhstan"
                },
                new CountryModel
                {
                    Id = 89,
                    Name = "Kenya"
                },
                new CountryModel
                {
                    Id = 90,
                    Name = "Kiribati"
                },
                new CountryModel
                {
                    Id = 91,
                    Name = "Kuwait"
                },
                new CountryModel
                {
                    Id = 92,
                    Name = "Kyrgyzstan"
                },
                new CountryModel
                {
                    Id = 93,
                    Name = "Laos"
                },
                new CountryModel
                {
                    Id = 94,
                    Name = "Latvia"
                },
                new CountryModel
                {
                    Id = 95,
                    Name = "Lebanon"
                },
                new CountryModel
                {
                    Id = 96,
                    Name = "Lesotho"
                },
                new CountryModel
                {
                    Id = 97,
                    Name = "Liberia"
                },
                new CountryModel
                {
                    Id = 98,
                    Name = "Libya"
                },
                new CountryModel
                {
                    Id = 99,
                    Name = "Liechtenstein"
                },
                new CountryModel
                {
                    Id = 100,
                    Name = "Lithuania"
                },
                new CountryModel
                {
                    Id = 101,
                    Name = "Luxembourg"
                },
                new CountryModel
                {
                    Id = 102,
                    Name = "Madagascar"
                },
                new CountryModel
                {
                    Id = 103,
                    Name = "Malawi"
                },
                new CountryModel
                {
                    Id = 104,
                    Name = "Malaysia"
                },
                new CountryModel
                {
                    Id = 105,
                    Name = "Maldives"
                },
                new CountryModel
                {
                    Id = 106,
                    Name = "Mali"
                },
                new CountryModel
                {
                    Id = 107,
                    Name = "Malta"
                },
                new CountryModel
                {
                    Id = 108,
                    Name = "Marshall Islands"
                },
                new CountryModel
                {
                    Id = 109,
                    Name = "Mauritania"
                },
                new CountryModel
                {
                    Id = 110,
                    Name = "Mauritius"
                },
                new CountryModel
                {
                    Id = 111,
                    Name = "Mexico"
                },
                new CountryModel
                {
                    Id = 112,
                    Name = "Micronesia"
                },
                new CountryModel
                {
                    Id = 113,
                    Name = "Moldova"
                },
                new CountryModel
                {
                    Id = 114,
                    Name = "Monaco"
                },
                new CountryModel
                {
                    Id = 115,
                    Name = "Mongolia"
                },
                new CountryModel
                {
                    Id = 116,
                    Name = "Montenegro"
                },
                new CountryModel
                {
                    Id = 117,
                    Name = "Morocco"
                },
                new CountryModel
                {
                    Id = 118,
                    Name = "Mozambique"
                },
                new CountryModel
                {
                    Id = 119,
                    Name = "Myanmar"
                },
                new CountryModel
                {
                    Id = 120,
                    Name = "Namibia"
                },
                new CountryModel
                {
                    Id = 121,
                    Name = "Nauru"
                },
                new CountryModel
                {
                    Id = 122,
                    Name = "Nepal"
                },
                new CountryModel
                {
                    Id = 123,
                    Name = "Netherlands"
                },
                new CountryModel
                {
                    Id = 124,
                    Name = "New Zealand"
                },
                new CountryModel
                {
                    Id = 125,
                    Name = "Nicaragua"
                },
                new CountryModel
                {
                    Id = 126,
                    Name = "Niger"
                },
                new CountryModel
                {
                    Id = 127,
                    Name = "Nigeria"
                },
                new CountryModel
                {
                    Id = 128,
                    Name = "North Korea"
                },
                new CountryModel
                {
                    Id = 129,
                    Name = "North Macedonia"
                },
                new CountryModel
                {
                    Id = 130,
                    Name = "Norway"
                },
                new CountryModel
                {
                    Id = 131,
                    Name = "Oman"
                },
                new CountryModel
                {
                    Id = 132,
                    Name = "Pakistan"
                },
                new CountryModel
                {
                    Id = 133,
                    Name = "Palau"
                },
                new CountryModel
                {
                    Id = 134,
                    Name = "Panama"
                },
                new CountryModel
                {
                    Id = 135,
                    Name = "Papua New Guinea"
                },
                new CountryModel
                {
                    Id = 136,
                    Name = "Paraguay"
                },
                new CountryModel
                {
                    Id = 137,
                    Name = "Peru"
                },
                new CountryModel
                {
                    Id = 138,
                    Name = "Philippines"
                },
                new CountryModel
                {
                    Id = 139,
                    Name = "Poland"
                },
                new CountryModel
                {
                    Id = 140,
                    Name = "Portugal"
                },
                new CountryModel
                {
                    Id = 141,
                    Name = "Qatar"
                },
                new CountryModel
                {
                    Id = 142,
                    Name = "Romania"
                },
                new CountryModel
                {
                    Id = 143,
                    Name = "Russia"
                },
                new CountryModel
                {
                    Id = 144,
                    Name = "Rwanda"
                },
                new CountryModel
                {
                    Id = 145,
                    Name = "Saint Kitts & Nevis"
                },
                new CountryModel
                {
                    Id = 146,
                    Name = "Saint Lucia"
                },
                new CountryModel
                {
                    Id = 147,
                    Name = "Samoa"
                },
                new CountryModel
                {
                    Id = 148,
                    Name = "San Marino"
                },
                new CountryModel
                {
                    Id = 149,
                    Name = "Sao Tome & Principe	"
                },
                new CountryModel
                {
                    Id = 150,
                    Name = "Saudi Arabia"
                },
                new CountryModel
                {
                    Id = 151,
                    Name = "Senegal"
                },
                new CountryModel
                {
                    Id = 152,
                    Name = "Serbia"
                },
                new CountryModel
                {
                    Id = 153,
                    Name = "Seychelles"
                },
                new CountryModel
                {
                    Id = 154,
                    Name = "Sierra Leone"
                },
                new CountryModel
                {
                    Id = 155,
                    Name = "Singapore"
                },
                new CountryModel
                {
                    Id = 156,
                    Name = "Slovakia"
                },
                new CountryModel
                {
                    Id = 157,
                    Name = "Slovenia"
                },
                new CountryModel
                {
                    Id = 158,
                    Name = "Solomon Islands"
                },
                new CountryModel
                {
                    Id = 159,
                    Name = "Somalia"
                },
                new CountryModel
                {
                    Id = 160,
                    Name = "South Africa"
                },
                new CountryModel
                {
                    Id = 161,
                    Name = "South Korea"
                },
                new CountryModel
                {
                    Id = 162,
                    Name = "South Sudan"
                },
                new CountryModel
                {
                    Id = 163,
                    Name = "Spain"
                },
                new CountryModel
                {
                    Id = 164,
                    Name = "Sri Lanka"
                },
                new CountryModel
                {
                    Id = 165,
                    Name = "St. Vincent & Grenadines"
                },
                new CountryModel
                {
                    Id = 166,
                    Name = "State of Palestine"
                },
                new CountryModel
                {
                    Id = 167,
                    Name = "Sudan"
                },
                new CountryModel
                {
                    Id = 168,
                    Name = "Suriname"
                },
                new CountryModel
                {
                    Id = 169,
                    Name = "Sweden"
                },
                new CountryModel
                {
                    Id = 170,
                    Name = "Switzerland"
                },
                new CountryModel
                {
                    Id = 171,
                    Name = "Syria"
                },
                new CountryModel
                {
                    Id = 172,
                    Name = "Tajikistan"
                },
                new CountryModel
                {
                    Id = 173,
                    Name = "Tanzania"
                },
                new CountryModel
                {
                    Id = 174,
                    Name = "Thailand"
                },
                new CountryModel
                {
                    Id = 175,
                    Name = "Timor-Leste"
                },
                new CountryModel
                {
                    Id = 176,
                    Name = "Togo"
                },
                new CountryModel
                {
                    Id = 177,
                    Name = "Tonga"
                },
                new CountryModel
                {
                    Id = 178,
                    Name = "Trinidad and Tobago"
                },
                new CountryModel
                {
                    Id = 179,
                    Name = "Tunisia"
                },
                new CountryModel
                {
                    Id = 180,
                    Name = "Turkey"
                },
                new CountryModel
                {
                    Id = 181,
                    Name = "Turkmenistan"
                },
                new CountryModel
                {
                    Id = 182,
                    Name = "Tuvalu"
                },
                new CountryModel
                {
                    Id = 183,
                    Name = "Uganda"
                },
                new CountryModel
                {
                    Id = 184,
                    Name = "Ukraine"
                },
                new CountryModel
                {
                    Id = 185,
                    Name = "United Arab Emirates"
                },
                new CountryModel
                {
                    Id = 186,
                    Name = "United Kingdom"
                },
                new CountryModel
                {
                    Id = 187,
                    Name = "United States"
                },
                new CountryModel
                {
                    Id = 188,
                    Name = "Uruguay"
                },
                new CountryModel
                {
                    Id = 189,
                    Name = "Uzbekistan"
                },
                new CountryModel
                {
                    Id = 190,
                    Name = "Vanuatu"
                },
                new CountryModel
                {
                    Id = 191,
                    Name = "Venezuela"
                },
                new CountryModel
                {
                    Id = 192,
                    Name = "Vietnam"
                },
                new CountryModel
                {
                    Id = 193,
                    Name = "Yemen"
                },
                new CountryModel
                {
                    Id = 194,
                    Name = "Zambia"
                },
                new CountryModel
                {
                    Id = 195,
                    Name = "Zimbabwe"
                }
                );
        }
    }
}
