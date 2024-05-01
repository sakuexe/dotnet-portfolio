using fullstack_portfolio.Models;

namespace fullstack_portfolio.Data;

public static class SeedDatabase
{
    private static IConfiguration? Config { get; set; }

    public static void Seed(IConfiguration config)
    {
        Config = config;
        AddAdminUser();
        AddExpertises();
        AddSkills();
        AddProjects(); // Add default descriptions
        AddExperiences();
        AddContactInfo();
    }

    private static void AddAdminUser()
    {
        if (MongoContext.GetAll<User>().Result.Count > 0)
            return;

        // make sure that the admin user is always set and encrypted
        string username = Config?.GetSection("Admin")["Username"]
            ?? throw new Exception("Admin username not provided");
        string password = Config?.GetSection("Admin")["Password"]
            ?? throw new Exception("Admin password not provided");

        User basedAdmin = new()
        {
            Username = username,
            Password = PasswordHasher.HashPassword(password),
            IsAdmin = true
        };
        MongoContext.Save(basedAdmin);
    }

    private static void AddExpertises()
    {
        if (MongoContext.GetAll<Expertise>().Result.Count > 0)
            return;

        List<Expertise> expertises = new();
        expertises.Add(new Expertise
        {
            Title = "Fullstack Web Development",
            Description = @"Passion towards Web Development and
                love to work on both Frontend and Backend. Experience
                working with many different frameworks and languages"
        });
        expertises.Add(new Expertise
        {
            Title = "Mobile Development",
            Description = @"Experience with making Mobile Applications.
                Kotlin for Android and React Native for crossplatform.
                Some experience with tools like Expo and Flutter."
        });
        expertises.Add(new Expertise
        {
            Title = "UI/UX Design",
            Description = @"Professional background in Graphic Design
                has built a strong foundation for UI/UX Design.
                Experience with Adobe Software and Figma."
        });

        MongoContext.SaveMultiple(expertises);
    }

    private static void AddSkills()
    {
        if (MongoContext.GetAll<Skill>().Result.Count > 0)
            return;

        List<Skill> skills = new();
        // General Skills
        skills.Add(new Skill
        {
            Title = "Frontend Development",
            Value = 82,
            Type = SkillType.Bar
        });
        skills.Add(new Skill
        {
            Title = "Backend Development",
            Value = 75,
            Type = SkillType.Bar
        });
        skills.Add(new Skill
        {
            Title = "Mobile Development",
            Value = 65,
            Type = SkillType.Bar
        });
        // Pies
        skills.Add(new Skill
        {
            Title = "Typescript",
            Value = 80,
            Type = SkillType.Pie
        });
        skills.Add(new Skill
        {
            Title = "C#",
            Value = 69,
            Type = SkillType.Pie
        });
        skills.Add(new Skill
        {
            Title = "Python",
            Value = 78,
            Type = SkillType.Pie
        });
        skills.Add(new Skill
        {
            Title = "Node.js",
            Value = 74,
            Type = SkillType.Pie
        });
        skills.Add(new Skill
        {
            Title = "Adobe Software",
            Value = 80,
            Type = SkillType.Pie
        });
        skills.Add(new Skill
        {
            Title = "Figma & UI/UX Design",
            Value = 62,
            Type = SkillType.Pie
        });

        MongoContext.SaveMultiple(skills);
    }

    public static void AddProjects()
    {
        if (MongoContext.GetAll<Project>().Result.Count > 0)
            return;

        List<Project> projects = new();

        projects.Add(new Project
        {
            Title = "Kantolan KunnonSali",
            Category = CategoryType.WebDevelopment,
            FinishedAt = new DateOnly(2022, 07, 31),
            Tags = new List<string> { "HTML/CSS", "Bootstrap", "Frontend", "cSchool HAMK" },
            Links = new List<string> { "source code;https://github.com/sakuexe/KantolanKunnonsali",
            "website;https://www.kantolankunnonsali.com/" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Wais Atifi", Role = "Developer" },
            },
        });
        projects.Add(new Project
        {
            Title = "Business Card",
            Category = CategoryType.GraphicDesign,
            FinishedAt = new DateOnly(2022, 04, 8),
            Tags = new List<string> { "Illustrator", "Branding", "Persona 5" },
            Links = new List<string> { "Image:https://www.linkedin.com/in/sakukarttunen/overlay/1635517772774/single-media-viewer/?profileId=ACoAADU3PKcBlKR8STr21xA4S4qQ30-TvoFE4EM" },
        });
        projects.Add(new Project
        {
            Title = "Joat Rakennus Oy",
            Category = CategoryType.WebDevelopment,
            FinishedAt = new DateOnly(2022, 10, 15),
            Tags = new List<string> { "HTML/CSS", "Bootstrap", "Frontend", "Freelance" },
            Links = new List<string> { "website;https://www.joat.fi/" },
        });
        projects.Add(new Project
        {
            Title = "He-Ti Huolto",
            Category = CategoryType.WebDevelopment,
            FinishedAt = new DateOnly(2023, 9, 12),
            Tags = new List<string> { "AstroJS", "TailwindCSS", "Frontend", "Freelance" },
            Links = new List<string> { "source code;https://github.com/sakuexe/he-tihuolto",
            "website;https://hetihuolto.com/" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Miia Raussi", Role = "Content Manager",
                Link = "https://www.linkedin.com/in/miia-raussi-ba3814187/" },
            },
        });
        projects.Add(new Project
        {
            Title = "Korpilahden Kesäralli 2021",
            Category = CategoryType.GraphicDesign,
            FinishedAt = new DateOnly(2021, 2, 28),
            Tags = new List<string> { "Illustrator", "Sketching", "Poster Design", "Freelance" },
            Links = new List<string> { "Images;https://jesseetelaniemi.kuvat.fi/kuvat/2021+Kuvat/Korpilahden+Kes%C3%A4ralli+12.6.2021+|+HRT/",
            "Video;https://www.youtube.com/watch?v=cvHvETl9ryY" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Pentti Nikander", Role = "Rally Expert" },
            },
        });
        projects.Add(new Project
        {
            Title = "Miia Raussi - Juontaja",
            Category = CategoryType.WebDevelopment,
            FinishedAt = new DateOnly(2023, 6, 12),
            Tags = new List<string> { "AstroJS", "TailwindCSS", "Frontend", "Freelance" },
            Links = new List<string> { "source code;https://github.com/sakuexe/miiajuonto",
            "website;https://miiaraussi.com/" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Miia Raussi", Role = "Content Manager",
                Link = "https://www.linkedin.com/in/miia-raussi-ba3814187/" },
            },
        });
        projects.Add(new Project
        {
            Title = "Helmiälä Puoti",
            Category = CategoryType.WebDevelopment,
            FinishedAt = new DateOnly(2024, 3, 21),
            Tags = new List<string> { "Django", "TailwindCSS", "Fullstack", "CMS", "Docker", "SEO" },
            Links = new List<string> { "source code;https://github.com/sakuexe/helmiala-puoti",
            "website;https://www.helmiala.com/" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Teemu Vilkman", Role = "Photography", Link = "https://www.instagram.com/vilkmedia/" },
                new TeamMember { Name = "Pia Vilkman", Role = "Ideas & Content" },
                new TeamMember { Name = "Minna Taiponen", Role = "Ideas & Content" },
            },
        });
        projects.Add(new Project
        {
            Title = "Fullstack Portfolio",
            Category = CategoryType.WebDevelopment,
            Description = @"The website you are currently viewing. I made the initial version in AstroJS,
            but later I got the chance to dedicate a lot more time to it at school. I then chose to develop
            it as my fullstack project in the Fullstack Web Development -course.

            The website is made with .NET Core and it utilizes MongoDB as its database. In the Frontend
            I used TailwindCSS and vanilla CSS. The website is hosted on three docker containers, running
            on a virtual machine. This is organized by utilizing Docker Compose, which makses it easy to
            manage the multiple containers that I have running. These are the dotnet webapp, the mongodb 
            database and caddy as the reverse proxy. Caddy also takes care of SSL certificates.

            There is an admin dashboard which lets me easily manage the content on the website. It is
            behing authorization, so only I can access it. The passwords are also hashed and salted for
            security reasons. Let me know how you like the website through the contact form! (written in
            blazor btw)",
            FinishedAt = new DateOnly(2024, 5, 5),
            Tags = new List<string> { ".NetCore", "TailwindCSS", "Fullstack", "CMS", "Docker" },
            Links = new List<string> { "source code;https://github.com/sakuexe/fullstack-portfolio",
            "website;https://www.sakukarttunen.com/" },
        });

        MongoContext.SaveMultiple(projects);
    }

    public static void AddExperiences()
    {
        if (MongoContext.GetAll<Experience>().Result.Count > 0)
            return;

        List<Experience> experiences = new();

        experiences.Add(new Experience
        {
            Title = "ICT-Engineer",
            Subtitle = "📌 HAMK Riihimäki",
            StartDate = new DateOnly(2021, 8, 15),
            Description = @"I am currently a third year ICT-Engineering student at
            HAMK Riihimäki. I started in 2021 and I am profiling my skills
            towards becoming a software engineer.",
            Skills = new List<string> { "Programming", "Software Development",
                "Report Writing", "DevOps", "Mathematics" }
        });
        experiences.Add(new Experience
        {
            Title = "Freelance Developer & Designer",
            Subtitle = "📌 Self-Employed, Remote",
            StartDate = new DateOnly(2019, 2, 12),
            Skills = new List<string> { "Web Development", "UI/UX Design",
            "AstroJS", "TailwindCSS", "Graphic Design" },
        });
        experiences.Add(new Experience
        {
            Title = "Service Designer & Technical Researcher",
            Subtitle = "📌 ORK Hämeenlinna, Hybrid",
            StartDate = new DateOnly(2023, 10, 21),
            EndDate = new DateOnly(2024, 1, 31),
            Description = @"Worked in the Service Design team at ORK. 
            Designed different kinds of templates and PowerPoints that were
            to be used when starting new processes. Also did some technical
            research such as sustainability and accessibility in ORK.",
            Skills = new List<string> { "Service Design", "Technical Research",
            "PowerPoint", "Sustainability", "Accessibility" }
        });
        experiences.Add(new Experience
        {
            Title = "Logistics & Transportation Personell",
            Subtitle = "📌 Tmi Tran Minh Tuan, Riihimäki, On-site",
            StartDate = new DateOnly(2021, 6, 12),
            EndDate = new DateOnly(2021, 8, 12),
            Description = @"I worked as a logistics driver as well as a storage worker.
            The company was a small firm located in Riihimäki that specialized in
            selling berries, vegetables and other summer-goods. The day consisted of
            early mornings, sometimes at around midnight, and sporadic schedules due
            to the seasonal nature of the part-time job.",
            Skills = new List<string> { "Driving", "Logistics", "Early Mornings" }
        });

        MongoContext.SaveMultiple(experiences);
    }

    public static void AddContactInfo()
    {
        if (MongoContext.GetAll<ContactInfo>().Result.Count > 0)
            return;

        ContactInfo contactInfo = new()
        {
            Email = "saku.karttunen@gmail.com",
            Links = new List<string> {
                "https://github.com/sakuexe?tab=repositories",
                "https://www.instagram.com/saku.karttunen/",
                "https://www.linkedin.com/in/sakukarttunen/",
            }
        };

        MongoContext.Save(contactInfo);
    }
}
