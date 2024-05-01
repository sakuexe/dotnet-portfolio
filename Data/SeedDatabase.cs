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
        AddProjects();
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
            Description = string.Join(" ",
                "Passion towards Web Development and",
                "love to work on both Frontend and Backend. Experience",
                "working with many different frameworks and languages")
        });
        expertises.Add(new Expertise
        {
            Title = "Mobile Development",
            Description = string.Join(" ",
                "Experience with making Mobile Applications.",
                "Kotlin for Android and React Native for crossplatform.",
                "Some experience with tools like Expo and Flutter.")
        });
        expertises.Add(new Expertise
        {
            Title = "UI/UX Design",
            Description = string.Join(" ",
                "Professional background in Graphic Design",
                "has built a strong foundation for UI/UX Design.",
                "Experience with Adobe Software and Figma.")
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
            Description = string.Join(" ",
            "A website for a local gym in Hämeenlinna. The website is made with plain HTML and CSS,",
            "with some Bootstrap in the mix. The business had a prior website, but it was horribly",
            "outdated and not mobile-friendly in the slightest. So the plan for me and my classmate",
            "was to make the new website user-friendly and as responsive as we could with our limited",
            "knowledge and experience. The end result turned out nicely and the client was very happy",
            "with the result and is using the website to this day.",
            "\n",
            "The project was part of HAMK's cSchool program, which was a short month and a half course,",
            "for the summer of 2022. I got to work with my classmate and got to learn a lot about working",
            "with a client and how to manage a project. This was also the first real project that I got",
            "to work on together with another developer, so I learned a lot about version control an",
            "co-operative coding. Thank you Wais and Kerttu for the great experience!",
            "\n",
            "The website is completely static and is hosted on the client's prior Elisa web hotel. The",
            "only way to get a hold of the backend was to use a FTP client and upload the files manually.",
            "This was a bit of a hassle, but as we were only first year students at the time, we didn't",
            "really even know what backend was. :D"),
            FinishedAt = new DateOnly(2022, 07, 31),
            Tags = new List<string> { "HTML/CSS", "Bootstrap", "Frontend", "cSchool HAMK" },
            Links = new List<string> { "source code;https://github.com/sakuexe/KantolanKunnonsali",
            "website;https://www.kantolankunnonsali.com/" },
            Team = new List<TeamMember> {
                new TeamMember { Name = "Wais Atifi", Role = "Developer", 
                    Link = "https://github.com/Waisatifi" },
            },
        });
        projects.Add(new Project
        {
            Title = "Business Card",
            Category = CategoryType.GraphicDesign,
            Description = string.Join(" ",
            "A business card design I created for myself. I got inspired form my playthrough of Persona 5",
            "and I thought that making a 'calling card' for myself would be a fun project. The project was",
            "quite fast and interesting and I am still using the design to this day. I still have some from",
            "the original batch!",
            "\n",
            "I used Adobe Photoshop for sketching the design and coming up with the layout. I then used Adobe",
            "Illustrator to create the actual vector graphics of the card. I used CMYK colors and tried to make",
            "sure that they would be bright enough for the print. In the next batch I will need to make it a bit",
            "more bright though.",
            "\n",
            "Finally I sent the pdf files with the design and 3mm bleed to a printing company that I found online.",
            "The end result was very nice and the paper felt premium quality. There were no issues with the print",
            "process."),
            FinishedAt = new DateOnly(2022, 04, 8),
            Tags = new List<string> { "Illustrator", "Branding", "Persona 5" },
            Links = new List<string> { "Image:https://www.linkedin.com/in/sakukarttunen/overlay/1635517772774/single-media-viewer/?profileId=ACoAADU3PKcBlKR8STr21xA4S4qQ30-TvoFE4EM" },
        });
        projects.Add(new Project
        {
            Title = "Joat Rakennus Oy",
            Category = CategoryType.WebDevelopment,
            Description = string.Join(" ",
            "Joat Rakennus Oy is a local construction company in Hämeenlinna. They were looking for someone",
            "to build them a website for their company. I got on a zoom call with the owner and we quickly",
            "agreed on the terms and scale of the project. The website was to be simple and informative, with",
            "only a couple of pages. Frontpage with information about the values and services of the company,",
            "a gallery page with images of their previous projects and a contact page simple contact information.",
            "\n",
            "This was my first ever client project in the field of web development. I had previously done some",
            "freelance graphic design work, but this was the first time I got to work on a website. I was very",
            "excited and nervous at the same time. I knew the basics of HTML and CSS, but how would it differ",
            "once it was an actual project? I used my limited experience and knowledge to build a website that",
            "would be both simple, informative and responsive on all screen sizes.",
            "\n",
            "I made the website with plain HTML and CSS, with some Bootstrap for the styling. The website is",
            "completely static and is hosted on a web hotel provided by the DomainHotelli website. So the project",
            "was just pure frontend. As it was a very early part of my career, I didn't know about backend or",
            "even about frontend frameworks, such as React or Vue. I was just excited to get to work on a real",
            "project.",
            "\n",
            "The client was very happy with the result and the website is still in use to this day. I have since",
            "learned a lot more about web development, so it is nice and nostalgic to look back at this project.",
            "Although I am not fond of actually updating it thought :D. I am very grateful for the opportunity."),
            FinishedAt = new DateOnly(2022, 10, 15),
            Tags = new List<string> { "HTML/CSS", "Bootstrap", "Frontend", "Freelance" },
            Links = new List<string> { "website;https://www.joat.fi/" },
        });
        projects.Add(new Project
        {
            Title = "He-Ti Huolto",
            Category = CategoryType.WebDevelopment,
            Description = string.Join(" ",
            "He-Ti Huolto is a company based in Hämeenlinna and they were looking to improve their online presence,",
            "as well as their SEO and marketing. I got to create a website for them as well as their logo. Which is",
            "used on the website and all over their branding. If you are in Hämeenlinna and look around the apartment",
            "buildings, you might see their logo on the doors of the buildings, created by me.",
            "\n",
            "The logo was a straight forward process. They had an idea of what they wanted and a reference photo.",
            "It was one of those lucky cases where the idea struck me immediately and I was able to get it right on",
            "(basically) the first try. The end result was nice and the client was very happy with it.",
            "\n",
            "The website I built with AstroJS, utilizing TailwindCSS for the styling, since tailwind is my favorite",
            "CSS library to work with. This is because it allows me to iterate and update the design very quickly,",
            "while maintaining a consistent look and feel. The website is a static frontend, hosted in a web hotel.",
            "AstroJS generates the site into static HTML, which by default includes ZERO Javascript. This is great",
            "for SEO and performance. It also allowed me to use JS frameworks in the development while keeping the",
            "build size small, lightweight and fast.",
            "\n",
            "The clients were very happy with the result and the publishing the website was a breeze. It was also the",
            "first time I got to use AstroJS in a client process and it has since become my favorite frontend framework",
            "to work with. The source code is freely accessible on my GitHub, so feel free to check it out!"),
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
            Description = string.Join(" ",
            "A poster design as well as a logo for the Korpilahti Summer Rally 2021. I got the opportunity to work",
            "with them as my grandfather is a rally enthusiast and he knew the people organizing the event. So when",
            "they were looking for a designer, he recommended me. I was very excited to get to work on this rally poster,",
            "as it felt like the biggest opportunity I had gotten so far. I created the logo and the poster design for the",
            "event in the winter as well, but I am more proud of the desing on the summer edition.",
            "\n",
            "I was serving my mandatory military service at the time that I got the opportunity. So I had to work on it",
            "during the evenings. Since it was during peak Corona virus times, I was not able to get home from for",
            "four weeks at a time. So I had to work on the design in my bed when I had the chance. I created the sketch",
            "on my Samsung tablet using Procreate. Which was enjoyable as I had just purchased the tablet and was eager",
            "to try it out in a real project. I then transferred the sketch to my computer and started working on the",
            "final design in Adobe Illustrator when I finally got home. It took quite a bit of time to get the design",
            "right, since it was quite detailed and colorful, compared to my previous works.",
            "\n",
            "The client really enjoyed the end result and the posters and logos were printed and used in the event. I",
            "have a link on this 'blog' post to some images from the event and a video from the rally. In them you can",
            "see the logo on the sides of the car clearly. Which is a huge honor for me as a motorsport fan. I am glad",
            "to have been a part of this event, even if it was in a small way. Thank you to everyone who made it possible!"),
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
            Description = string.Join(" ",
            "I got to create a website for my Mom's presenter business. She wanted to have a website that she could",
            "easily link to potential clients and that would showcase her skills and services. I was happy to",
            "help her out and once we got the basics down of what the project was going to include, I got to work.",
            "\n",
            "I decided to use AstroJS as the framework, since the website was going to be quite simple and I wanted",
            "to have it be performant and SEO friendly. AstroJS is a fantastic framework for static websites and it",
            "has a great developer experience. Thank you Peter for recommending it to me! I also used my trusty TailwindCSS",
            "(also thanks to Peter for recommending it) for the styling. I had used it in a couple of projects before",
            "and I knew that it wouldn't let me down this time either. In eccense the website is a static frontend,",
            "hosted on a web hotel. The website is very fast and SEO friendly and uses just some JSON files for the",
            "content. It has been good enough and fast for a project of this size.",
            "\n",
            "The end result was modern and clean while still having a lot of personality. The colors are easy for the",
            "eyes and there's a lot of small animations and transitions that make the website feel alive. The client",
            "(my mom) was happy with the result and the project was quite fast to complete, since I had a clear vision",
            "of what I wanted it to look like. Thanks to the great color palette, it was easy to make the design look",
            "like I envisioned. I am content with the result."),
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
            Description = string.Join(" ",
            "I got a chance to build a website for a local startup in Hämeenlinna, as I was recommended by to the owners.",
            "They were family friends, so I was very happy to help them however I could. The business was just starting",
            "and they wanted to have a website to showcase their products and services, as well as provide a basis for",
            "their brand image online. I was excited to work on the project, since after talking with the owners, I realized",
            "that it was going to be a fullstack project. Since they wanted to have a way to quickly manage and maintain the",
            "contents on the website.",
            "\n",
            "I decided to use Django as the fullstack web framework, since I had some prior experience with it and I knew that",
            "it would allow for fast and reliable development. It is battle-tested and provides a whole bunch of tools and",
            "services right out of the box that would be useful for the project. I also used TailwindCSS as it it personally",
            "my favorite styling solution and can provide me with the fastest way to iterate and develop the frontend.",
            "I had worked on a project with two classmates before, where we used Django, so I was excited to return to it",
            "and see how I could implement new skills that I had learned since.",
            "\n",
            "The website is hosted on a VPS on DigitalOcean, running two docker containers. One for the Django webapp and one",
            "for the Nginx reverse proxy. The database is a SQLite database, which came built in with Django. It also",
            "allowed for easy backups with docker volumes and mounts. The website is also behind a Cloudflare CDN, which",
            "provides SSL certificates and DDoS protection as well as static file caching. I tried to optimize the website",
            "for SEO and performance, while keeping the design easy to use and clean. Overall the tech stack was very fun",
            "and I learned a whole lot during the process. I am very glad that I went with Docker and Cloudflare, as they",
            "made the deployment and maintenance of the website very easy. Django was definitely not too slow!",
            "\n",
            "The clients were very happy with the result and while the website has had some errors and bugs that were inevitable",
            "with my first fullstack project, they have been understanding and patient. The end result is a website that runs fast",
            "is scalable, easy to maintain and has good SEO. I am glad that I got to work on this project and I am very proud of the",
            "team that I got to work with. The site has gotten more than 2000 unique visitors and over 500 google search clicks",
            "in it's first month of being published. Thank you to everyone who made it possible!"),
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
            Description = string.Join(" ",
            "The website you are currently viewing. I made the initial version in AstroJS,",
            "but later I got the chance to dedicate a lot more time to it at school. I then chose to develop",
            "it as my fullstack project in the Fullstack Web Development -course.",
            "\n",
            "The website is made with .NET Core and it utilizes MongoDB as its database. In the Frontend",
            "I used TailwindCSS and vanilla CSS. The website is hosted on three docker containers, running",
            "on a virtual machine. This is organized by utilizing Docker Compose, which makses it easy to",
            "manage the multiple containers that I have running. These are the dotnet webapp, the mongodb ",
            "database and caddy as the reverse proxy. Caddy also takes care of SSL certificates.",
            "\n",
            "There is an admin dashboard which lets me easily manage the content on the website. It is",
            "behing authorization, so only I can access it. The passwords are also hashed and salted for",
            "security reasons. Let me know how you like the website through the contact form! (written in",
            "blazor btw)"),
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
            Description = string.Join(" ", 
            "I am currently a third year ICT-Engineering student at",
            "HAMK Riihimäki. I started in 2021 and I am profiling my skills",
            "towards becoming a software engineer."),
            Skills = new List<string> { "Programming", "Software Development",
                "Report Writing", "DevOps", "Mathematics" }
        });
        experiences.Add(new Experience
        {
            Title = "Freelance Developer & Designer",
            Subtitle = "📌 Self-Employed, Remote",
            StartDate = new DateOnly(2019, 2, 12),
            Description = string.Join(" ",
            "I have worked since 2019 as a freelance graphic designer a midst my",
            "studies. since 2022 I have pivoted towards mostly working as a web",
            "developer. I have made websites and logos for all kinds of companies",
            "and people in Finland."),
            Skills = new List<string> { "Web Development", "UI/UX Design",
            "AstroJS", "TailwindCSS", "Graphic Design" },
        });
        experiences.Add(new Experience
        {
            Title = "Service Designer & Technical Researcher",
            Subtitle = "📌 ORK Hämeenlinna, Hybrid",
            StartDate = new DateOnly(2023, 10, 21),
            EndDate = new DateOnly(2024, 1, 31),
            Description = string.Join(" ",
            "Worked in the Service Design team at ORK.",
            "Designed different kinds of templates and PowerPoints that were",
            "to be used when starting new processes. Also did some technical",
            "research such as sustainability and accessibility in ORK."),
            Skills = new List<string> { "Service Design", "Technical Research",
            "PowerPoint", "Sustainability", "Accessibility" }
        });
        experiences.Add(new Experience
        {
            Title = "Logistics & Transportation Personell",
            Subtitle = "📌 Tmi Tran Minh Tuan, Riihimäki, On-site",
            StartDate = new DateOnly(2021, 6, 12),
            EndDate = new DateOnly(2021, 8, 12),
            Description = string.Join(" ",
            "I worked as a logistics driver as well as a storage worker.",
            "The company was a small firm located in Riihimäki that specialized in",
            "selling berries, vegetables and other summer-goods. The day consisted of",
            "early mornings, sometimes at around midnight, and sporadic schedules due",
            "to the seasonal nature of the part-time job."),
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
