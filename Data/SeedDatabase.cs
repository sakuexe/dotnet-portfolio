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
        AddProjects(); // finish this
        AddExperiences();
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
        // TODO: Add projects after the model is done
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
