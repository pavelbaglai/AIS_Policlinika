using System;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using DigitalMedicine.Models.MedCard;
using DigitalMedicine.Models.News;
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;

namespace DigitalMedicine.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DataBaseEntity")
        {
         // Database.SetInitializer<DataBaseContext>(new DataBaseInitializer());
        }

        public virtual DbSet<Institution.Institution> Institutions { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Labaratory> Labaratories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
        public virtual DbSet<OtherWorker> OtherWorkers { get; set; }
        public virtual DbSet<WorkTime> WorkTimes { get; set; }
        public virtual DbSet<ClinicNews> ClinicNews { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<LaboratoryAppointment> LaboratoryAppointments { get; set; }
        public virtual DbSet<LaboratoryReport> LabaratoryReports { get; set; }
        public virtual DbSet<LaboratoryAnalysis> LaboratoryAnalyses{ get; set; }
        public virtual DbSet<DoctorReport> DoctorReports { get; set; }
        public virtual DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public virtual DbSet<SymptomGroup> SymptomGroups { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<SymptomWeight> SymptomsWeights { get; set; }
    }

    public class DataBaseInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {

            #region Institutions

           // context.Pharmacies.Add(new Pharmacy() { Name = "Аптека Гаевского", Phone="+38(050)578-22-99",About = "Первая аптека в БД! У нас довольно-таки широкий ассортимент лекарств и низкие цены!", Address = "Садовая 19", Photo = File.ReadAllBytes(@"D:\Diplom\DigitalMedicine\DigitalMedicine\Photos\PharmacyDraw.png") , Latitude= 46.485324, Longitude=30.731064 });

            Clinic c1 = new Clinic() { Name = "БУЗ ВО ОКБ", Phone = "+7(473)207-24-00", About = "Воронежская областная клиническая больница №1 является крупнейшим в Центрально-Черноземном регионе больнично-поликлиническим учреждением, авторитетным лечебным и научным центром Российской Федерации, передовым учреждением региона по развитию высокотехнологичной медицинской помощи. Вся деятельность нашего коллектива направлена на сохранение Вашего здоровья. Мы предоставляем жителям Воронежской области качественную специализированную, в том числе высокотехнологичную медицинскую помощь с использованием самых передовых технологий и современных метод", Address = "394066, г. Воронеж, Московский проспект, 151, корпус 1, БУЗ ВО ВОКБ №1", Photo = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\Photos\Clinic1.jpg"), Latitude= 51.741559, Longitude = 39.178252 };
            context.Clinics.Add(c1);

            Labaratory l1 = new Labaratory() { Name = "ЛабСтори", Address =  "Санкт-Петербург, Наличная улица, 40 корпус 1", Phone = " +7(812)501-15-48", About = "Своевременное определение точной причины возникновения недуга – залог эффективного лечения. Обратившись в клинику «ЛабСтори» на Наличной улице, вы получите полную информацию о состоянии вашего организмом.", Photo = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\Photos\LaboratoryDraw.png") , Latitude= 59.949395, Longitude = 30.231459 };
            context.Labaratories.Add(l1);

            #endregion

            #region  Roles
            Role r1 = new Role() { Id = 1, Name = "Администратор системы", Description = "Главная роль в веб-приложении! Упавляет всей системой(добавляет/удаляет больницы,аптеки, лаборатории, назначает администратора клиники)." };
            Role r2 = new Role() { Id = 2, Name = "Администратор клиники", Description = "Регестрирует или удаляет модераторов клиники и врачей" };
            Role r3 = new Role() { Id = 3, Name = "Модератор клиники", Description = "Отвечает за информационную часть(новости, удаление комментариев, публикация прайс-листа услуг, изменение информации о клинике)" };
            Role r4 = new Role() { Id = 4, Name = "Доктор", Description = "Просмотр очереди на прием, мед.карты пациентов и добавление в неё записей" };
            Role r5 = new Role() { Id = 5, Name = "Лаборант", Description = "Просмотр мед.карты пациента и добавление анализов лаборатории" };
            Role r6 = new Role() { Id = 6, Name = "Пациент", Description = "Разрешен просмотр своей мед.карты и запись к врачу." };
            Role r7 = new Role() { Id = 7, Name = "Администратор лаборатории", Description = "Изменение информаци и добавление новых анализов и лаборантов." };
            context.Roles.AddRange(new List<Role>() { r1, r2, r3, r4, r5, r6 });

            #endregion

            #region DoctorSpecialities

            List<DoctorSpeciality> specialities = new List<DoctorSpeciality>() {
                new DoctorSpeciality(){ Name="Венеролог", Information="Врач, который занимается выявлением и лечением разного характера венерических заболеваний."},
                new DoctorSpeciality(){ Name="Вирусолог", Information="Врач, который получил подготовку в сфере медицинской бактериологии и осуществляет вирусологические исследование материалов от больных и объектов окружающей среды."},
                new DoctorSpeciality(){ Name="Гастроэнтеролог", Information="Врач, который специализируется на диагностике, профилактике и лечении различных заболеваний органов желудочно-кишечного тракта." },
                new DoctorSpeciality(){ Name="Гинеколог", Information="Врач, который специализируется на диагностике, лечении и предупреждении женских заболеваний" },
                new DoctorSpeciality(){ Name="Дерматолог", Information="Врач, который осуществляет лечение всех видов кожных заболеваний, обусловленных разными факторами. Очень часто при некоторых инфекционных болезнях поражается и кожа, - и в этом случае требуется консультация дерматолога." },
                new DoctorSpeciality(){ Name="Диетолог", Information="Врач-специалист, который занимается вопросами подготовки рационального питания, в том числе лечебного. Как правило, деятельность диетолога востребована в санаторно-курортных и больничных учреждениях." },
                new DoctorSpeciality(){ Name="Зубной врач", AbilityToPatientRecord=true, Information="Специалист в сфере лечения заболеваний зубов и полости рта." },
                new DoctorSpeciality(){ Name="Кардиолог", Information="Врач, который специализируется на проблемах сердца и сосудов, а также профилактике, диагностике и лечении таких заболеваний сердечно-сосудистой системы, как инфаркт миокарда, стенокардии, атеросклероза, аритмии и других." },
                new DoctorSpeciality(){ Name="Невролог", Information="Врач-специалист, который на профессиональном уровне осуществляет лечение, диагностику и профилактику заболеваний головного, спинного мозга и периферической нервной системы." },
                new DoctorSpeciality(){ Name="Невропатолог", Information="Врач, который ведет лечение заболеваний нервной системы, в том числе головного и спинного мозга, а также периферических нервов." },
                new DoctorSpeciality(){ Name="Окулист", AbilityToPatientRecord=true, Information="Врач, в специализацию которого входит диагностика и лечение заболеваний глаз и вспомогательных органов (слезных желез, век)." },
                new DoctorSpeciality(){ Name="Онколог", Information="Врач, который осуществляет диагностику и лечение доброкачественных и злокачественных новообразований: рака разных органов, предраковых заболеваний и рака кожи." },
                new DoctorSpeciality(){ Name="Проктолог", Information="Врач, который специализируется на лечении заболеваний прямой кишки и заднего прохода. При помощи различных терапевтических и хирургических средств проктолог осуществляет лечение геморроя, опухолей прямой кишки и трещин заднего прохода." },
                new DoctorSpeciality(){ Name="Психиатр", Information="Специалист, который занимается диагностикой, лечением и профилактикой психических заболеваний." },
                new DoctorSpeciality(){ Name="Ревматолог", Information="Врач, который специализируется на лечении заболеваний воспалительно-дистрофического характера, в результате которых поражаются соединительные суставы и ткани." },
                new DoctorSpeciality(){ Name="Семейный врач", AbilityToPatientRecord=true, Information="Специалист, который осуществляет наблюдение и лечение всех членов конкретной семьи. Стоит обозначить, что в преимущественном большинстве своем, семейный доктор может достаточно квалифицированно проконсультировать по большинству вопросов, связанных с состоянием здоровья." },
                new DoctorSpeciality(){ Name="Терапевт", Information="Врач, который имеет соответствующее образование по вопросам диагностики, профилактики и лечения болезней внутренних органов." },
                new DoctorSpeciality(){ Name="Токсиколог", Information="Врач-специалист, который ориентирует свою деятельность на осуществление диагностики, лечения и предупреждения отравлений различного характера (поражение отравляющими и вредными веществами)." },
                new DoctorSpeciality(){ Name="Травматолог", Information="Врач, занимающийся лечением травм и послетравмовых синдромов. Помимо этого достаточно часто в обязанности травматолога входит осуществление лечения плоскостопии, искривления позвоночника и нарушение форм стоп." },
                new DoctorSpeciality(){ Name="Уролог", Information="Специалист, который осуществляет диагностику, лечение и профилактику заболеваний органов мочеполовой системы." },
                new DoctorSpeciality(){ Name="Хирург", Information="Врач, который занимается заболеваниями, которые требуют оперативных методов лечения." },
                new DoctorSpeciality(){ Name="Эндокринолог", Information="Специалист, который компетентен осуществлять диагностику, профилактику и лечение заболеваний эндокринной системы." }
            };
            context.DoctorSpecialities.AddRange(specialities);

            #endregion

            #region Users

            Doctor d1 = new Doctor()
            {
                Name = "Анатолий",
                Surname = "Березовский",
                Patronymic = "Сергеевич",
                Role = r4,
                Birthday = new DateTime(1970, 3, 11),
                Information = "Имею большой опыт работы. Окончил Санкт-Петербургский медицинский государственный университет им. Павлова (СпбГМУ) в 1990 году. После этого работал в двух клиниках города Санкт-Петербурга.",
                Clinic = c1,
                Email = "bere-za@mail.ru",
                Login = "doctor1",
                Password = "doctor1",
                Photo = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\Photos\doctor1.png"),
                ReceptionTime = new TimeSpan(0, 30, 0),
                RotationTime = new TimeSpan(1, 20, 0),
                Specialities = new List<DoctorSpeciality>() { specialities[15] },
                WorkTime = new WorkTime() { Monday = "08:00-20:00", Tuesday = "08:00-20:00", Wednesday = "08:00-20:00", Thursday = "08:00-20:00", Friday = "08:00-20:00", Saturday = "12:00-15:00" }
            };
            context.Doctors.Add(d1);
            Doctor d2 = new Doctor()
            {
                Name = "Богдан",
                Surname = "Нефёдов",
                Patronymic = "Юрьевич",
                Role = r4,
                Birthday = new DateTime(1991, 8, 19),
                Information = "Молодой врач-практикант. Окончил Ростовский государственный медицинский университет(РостГМУ) в 2010 году. Работаю в клинике с 2012 года и лечу людей.",
                Clinic = c1,
                Email = "bogdan@ukr.net",
                Login = "doctor2",
                Password = "doctor2",
                Photo = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\Photos\doctor2.png"),
                ReceptionTime = new TimeSpan(0, 20, 0),
                RotationTime = new TimeSpan(0, 60, 0),
                Specialities = new List<DoctorSpeciality>() { specialities[2] },
                WorkTime = new WorkTime() { Monday = "10:00-18:30", Tuesday = "10:00-18:30", Wednesday = "10:00-18:30", Thursday = "10:00-18:30", Friday = "10:00-18:30" }
            };
            context.Doctors.Add(d2);

            Patient p1 = new Patient()
            {
                Name = "Ян",
                Surname = "Горшков",
                Patronymic = "Владимирович",
                Birthday = new DateTime(1998, 8, 21),
                Role = r6,
                Email = "gorshkov.yan1998@gmail.com",
                ConfirmedEmail = true,
                Login = "patient1",
                Password = "patient1",
                Photo = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\Photos\patient1.jpg")
            };
            context.Patients.Add(p1);

            User mainAdmin = new User()
            {
                Name = "Администратор",
                Surname = "Администраторов",
                Patronymic = "Администраторович",
                Birthday = new DateTime(1988, 2, 11),
                Role = r1,
                Email = "administrator@gmail.com",
                Login = "administrator",
                Password = "administrator",
            };
            context.Users.Add(mainAdmin);
            #endregion
            #region Reports

            LaboratoryReport lr1 = new LaboratoryReport()
            {
                Date = DateTime.Now,
                Labaratory = l1,
                Description = "Перелом кости ноги. Больно!",
                Files = File.ReadAllBytes(@"D:\поликлиника\диплом\DigitalMedicine\LabaratoryArchive\Report1.rar"),
                Patient = p1
            };
            context.LabaratoryReports.Add(lr1);

            #endregion

            #region Symptoms

            List<SymptomGroup> sgroups = new List<SymptomGroup>() {
                    new SymptomGroup(){Name="Глаза",Information="Глаз довольно часто сравнивают с фотоаппаратом, в котором имеется кожух (роговица), объектив (хрусталик), диафрагма (радужка) и светочувствительная пленка (сетчатка). Более уместно было бы сравнить глаз человека с аналогом сложнейшего компьютерного кабельного устройства, поскольку смотрим мы глазом, а видим мозгом." },
                    new SymptomGroup(){ Name="Голова", Information="Голова по праву считается главенствующей частью тела. Ведь в ней находится головной мозг, а также органы зрения, слуха, обоняния, вкуса, носоглотка, язык, жевательный аппарат."},
                    new SymptomGroup(){ Name="Грудь", Information="Грудная клетка, грудь — одна из частей туловища. Образуется грудиной, рёбрами, позвоночником, а также мышцами. Грудная клетка содержит в себе грудную полость, а также из-за изогнутости диафрагмы верхнюю часть брюшной полости. Укреплённая внутри и снаружи на грудной клетке дыхательная мускулатура обеспечивает человеку дыхание."},
                    new SymptomGroup(){ Name="Живот", Information="В брюшной области (или как называют пациенты - в животе) расположена главным образом система пищеварения. Печень, желудок и поджелудочная железа находятся в верхней части полости, которая еще защищена прикрывающей ее грудной клеткой."},
                    new SymptomGroup(){ Name="Кожа", Information="Кожа является самым большим органом в организме человека. Площадь кожи у среднестатистического взрослого человека составляет около 1.5-2.3 м^2, масса без подкожной жировой клетчатки (ПЖК) 4-6%, а вместе с ПЖК 16-17% от общего веса тела. На разных участках тела кожа различается по толщине, плотности, наличию и количеству сальных и потовых желез. "},
                    new SymptomGroup(){ Name="Ноги", Information="Ноги – это парный орган движения и опоры человека. Строение ног включает мышцы и сухожилия, костный скелет, сосуды (артерии, вены, капилляры) и нервные волокна (двигательные и чувствительные)."},
                    new SymptomGroup(){ Name="Нос и ротовая полость", Information="Нос и ротовая полость вместе с гортанью относятся к верхним дыхательным путям дыхательной системы."},
                    new SymptomGroup(){ Name="Общие симптомы", Information="Под общими подразумеваются те симптомы и признаки, которые затрагивают весь организм в целом или несколько органов сразу или те симптомы, проявление которых может быть характерно для разных органов или систем. В общем, в эту группу вошли все симптомы, которые невозможно четко классифицировать по положению в организме, и которые сильно влияют на общее самочувствие человека."},
                    new SymptomGroup(){ Name="Органы малого таза", Information="Малый таз – это анатомическое пространство, которое ограничено костями таза. Спереди полость ограничена лонным сочленением, с боков крыльями подвздошной кости, сзади костями крестца и копчика."},
                    new SymptomGroup(){ Name="Пах", Information="Пах, или паховая область - это часть нижнего края брюшной полости, которая примыкает к бедру. В паху находится паховый канал, через который проходят достаточно крупные кровеносные сосуды бедра и семенной канатик."},
                    new SymptomGroup(){ Name="Руки", Information="Рука - верхняя конечность человека. В руке выделяют следующие анатомические области и соответствующие им кости сверху вниз:"},
                    new SymptomGroup(){ Name="Спина", Information="Без должного ухода наша спина стареет гораздо раньше, чем появляются морщины на лице. И никакие салонные процедуры не вернут ее молодость."},
                    new SymptomGroup(){ Name="Уши", Information="Современные биологи все чаще говорят о том, что слуховой аппарат человека – самый совершенный из всех его органов чувств: в нем наблюдается наибольшая концентрация нервных клеток и окончаний (только в “улитке” – одной из частей слухового аппарата – насчитывается около 30 000 чувствительных клеток). Поэтому вполне определенно можно сказать, что ухо – самый чуткий орган чувств."},
                    new SymptomGroup(){ Name="Шея",  Information="Не смотря на то, что шея по объему - только малая часть человеческого организма, она содержит в себе множество жизненно важных структур. Из ротовой полости и носоглотки сюда ведут гортань, переходящая в трахею (обеспечивают дыхание), и пищевод."},
                    new SymptomGroup(){ Name="Ягодицы и анус", Information="Ягодицы – это парное анатомическое образование, состоящее из мягких тканей (мышцы, подкожная жировая клетчатка, кожа) и расположенное на задней и задне-боковой поверхности тела. Каждая ягодица ограничена сверху областью пояснично-крестцового ромба (ромб Михаэлиса) и крылом подвздошной кости, сбоку областью большого вертела на бедре, снизу подъягодичной складкой и промежностью."}
                };
            context.SymptomGroups.AddRange(sgroups);

            List<Symptom> eyesSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в глазу", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Слезоточивость глаз", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Двоение перед глазами", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Кровоизлияние в глаз", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Мешки под глазами", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Нервное подрагивание глаз (нервный тик)", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Ухудшение зрения", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Низкое зрение", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Опущение век", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Ощущение сухости в глазах", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Покраснение глаза", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Потемнение в глазах", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Снижение остроты зрения в сумерках, (куриная слепота)", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Сужение полей зрения", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="Экзофтальм (пучеглазие)", SymptomGroup=sgroups[0]}
                };
            List<Symptom> headSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Болит лицо", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Выпадение волос (алопеция, облысение, истончение волос)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Галлюцинации", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Головная боль (болит голова)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Головокружение", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Деменция (приобретенное слабоумие)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Дрожание головы (тремор)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Ломкость волос (ломкие, сухие, секущиеся волосы)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Нарушение когнитивных функций (памяти, речи, восприятия)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Обмороки", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Перхоть", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Потеря памяти, амнезия", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Приливы, ощущение жара", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Ухудшение памяти", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="Хроническая боль", SymptomGroup=sgroups[1]}
                };
            List<Symptom> breastSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боли в груди при дыхании", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Боли, дискомфорт, уплотнения в молочной железе", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Боль в груди (грудной клетке)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Боль в груди (молочной железе)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Боль в области сердца", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Выделения из груди у женщин", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Жжение в груди (грудной клетке)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Кашель", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Лёгочное сердце", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Нарушения ритма сердца", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Одышка", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Ощущение нехватки воздуха при дыхании", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Первые признаки инфаркта миокарда", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Удушье", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Учащенное дыхание", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="Учащенное сердцебиение", SymptomGroup=sgroups[2]}
                };
            List<Symptom> stomachSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боли в животе", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Вздутие живота (метеоризм)", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Диспепсия, несварение желудка", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Нарушения функции кишечника", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Отложение жира в области живота", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Расстройство желудка", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Рвота, рвота с кровью", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Снижение аппетита", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Тошнота", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="Тяжесть в желудке", SymptomGroup=sgroups[3]}
                };
            List<Symptom> skinSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Атрофия кожи (яма, вдавление на коже)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Веснушки", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Высыпания в виде папул (округлое образование, возвышается над уровнем кожи)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Высыпания в виде пустул (гнойников) на коже", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Гиперкератоз кожи", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Депигментация кожи, светлые пятна на коже", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Желтуха, пожелтение кожи", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Жирная кожа", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Зуд, чешется кожа", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Красно розовые пятнышки на коже (крапивница)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Красные узелки на коже, воспаление кожи и подкожной клетчатки", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Лихорадка (повышенная температура тела)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Морщины", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Пигментация кожи", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Повышенное потоотделение", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Пузыри на коже - высыпания в виде пузырьков и пузырей (везикул)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Пятна на коже (высыпания в виде пятен)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Рубец", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Симптомы псориаза", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Синяк, ушиб, кровоизлияние в кожу", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Сосудистые звездочки (телеангиоэктазии)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Сухая кожа", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Сыпь", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Угревая сыпь (угри на лице и теле)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Хлоазма (остуда, меланодермия, мелазма)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="Шишки, уплотнения на коже и под кожей", SymptomGroup=sgroups[4]}
                };
            List<Symptom> legsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в колене (коленном суставе)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Боль в ногах (боль в нижних конечностях, болят ноги)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Боль в пятке (болит пятка)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Боль в тазобедренном суставе", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Воспаление сустава", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Диабетическая стопа", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Нарушения походки", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Отёки на ногах (опухают ноги, отекают ноги)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Отеки нижних конечностей", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Ощущение жжения, покалывания, пульсирования, зуда в нижних конечностях", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Перемежающаяся хромота", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Покраснения на коже нижних конечностей", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Потемнение кожных покровов нижних конечностей", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Появление сухости, трещин и язв на коже нижних конечностей", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Симптомы варикозного расширения вен на ногах", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Судороги", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Тяжесть в ногах", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Усталость ног", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="Холодные на ощупь и бледные ноги", SymptomGroup=sgroups[5]}
                };
            List<Symptom> noseAndMouthSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Болезненность зуба от сладкого, холодного, горячего, при накусывании на зуб", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Боль в челюсти", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Заложенность носа, переходящая в насморк", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Затруднение носового дыхания", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Зуд в носу", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Изжога", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Изменение речи", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Кровь в мокроте: кашель с кровью, слюна с кровью", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Меловидное (белое) пятно на зубе", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Налет на языке", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Насморк", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Неприятный запах изо рта", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Носовое кровотечение", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Отрыжка", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Потемнение эмали зуба", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Сильные приступообразные ночные зубные боли", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Сопли", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Сухость во рту", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Трещины, «Заеды» в уголках рта", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Храп", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="Чихание", SymptomGroup=sgroups[6]}
            };
            List<Symptom> mainSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Анорексия (отсутствие аппетита)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Апатия, вялость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Боль", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Боль в мышцах (болят мышцы)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Боль в суставах", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Быстрая утомляемость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Высокая температура тела", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Жгучие боли, жжение в теле и конечностях", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Конвульсии", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Метеочувствительность, метеозависимость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Мышечная слабость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Нарушения сна", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Недостаток кальция", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Неконтролируемые движения веками, головой, плечами, втягивание живота", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Неутолимая постоянная жажда", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Низкий гемоглобин (анемия)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Ожирение", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Отеки", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Первые признаки и симптомы рака", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Повышение температуры тела", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Повышенное артериальное давление", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Повышенный холестерин", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Признаки сахарного диабета", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Приступы обжорства, неутолимый голод", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Проблемы настроения", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Раздражительность повышенная", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Снижение иммунитета", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Сонливость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Тревожность и беспокойство", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Усталость", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Утомление", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="Эндогенная депрессия", SymptomGroup=sgroups[7]},
                };
            List<Symptom> pelvicOrgansSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Аномальные вагинальные кровотечения", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Бели - необычные выделения из влагалища", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Боль внизу живота (болит низ живота)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Боль во время полового акта (секса)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Боль при менструации", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Задержка менструации", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Зуд влагалища", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Менструация", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Нарушение менструального цикла", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Нарушения мочеиспускания", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Не получается забеременеть", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Неприятный запах вагинальных выделений", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Нерегулярные месячные", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Обильная менструация", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Отсутствие менструации (нет месячных)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Отсутствие овуляции", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Отсутствие оргазма у женщин", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Повышенный тонус матки при беременности", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Снижение тонуса мышц тазового дна", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="Сухость влагалища", SymptomGroup=sgroups[8]}
            };
            List<Symptom> groinSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Анурия (отсутствие мочи)", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Белый налет на головке полового члена", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Бесплодие у мужчины", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Боли и дискомфорт в области промежности", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Боль в области половых органов у мужчин", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Боль в яичках", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Выделения из мочеиспускательного канала полового члена (уретры) у мужчин", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Жжение и боль при мочеиспускании", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Запах мочи", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Импотенция", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Красные пятна на головке члена у мужчин", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Кровь в моче", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Кровь в сперме", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Мутная моча. Помутнение мочи", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Недержание мочи", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Недостаточность эрекции для начала или продолжения полового акта", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Ранняя эякуляция", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="Частое мочеиспускание", SymptomGroup=sgroups[9]},
                };
            List<Symptom> handsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в плече (болит плечо)", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Боль в руке", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Желтые ногти", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Изменение цвета и формы ногтей", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Онемение пальцев рук", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Тремор (руки дрожат)", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="Холодные руки", SymptomGroup=sgroups[10]}
                };
            List<Symptom> backSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в пояснице, поясничной области", SymptomGroup=sgroups[11]},
                    new Symptom(){ Name="Боль в спине", SymptomGroup=sgroups[11]},
            };
            List<Symptom> earsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в ухе", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="Заложенность уха", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="Потеря слуха, ухудшение слуха", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="Снижение слуха (плохо слышат уши/ухо)", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="Тиннитус (звон и шум в ушах)", SymptomGroup=sgroups[12]}
                };
            List<Symptom> neckSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в горле", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Боль в шее, между лопаток", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Кровохарканье (мокрота с кровью), кашель с кровью", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Охриплость, потеря голоса", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Увеличение лимфоузлов", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Цервикалгия вертебогенная", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="Шишка на горле (увеличение щитовидной железы)", SymptomGroup=sgroups[13]}
                };
            List<Symptom> buttocksSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="Боль в аноректальной области, боль копчике", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Боль в крестце (сакродиния)", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Боль в области ануса", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Боль в области ягодиц", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Выпадение из анального канала", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Диарея (понос)", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Запор", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Зуд или выделения из анального прохода", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="Кровь в кале (стуле), кровотечение из заднего прохода (прямой кишки, ануса)", SymptomGroup=sgroups[14]}
                };

            context.Symptoms.AddRange(eyesSymptoms);
            context.Symptoms.AddRange(headSymptoms);
            context.Symptoms.AddRange(breastSymptoms);
            context.Symptoms.AddRange(stomachSymptoms);
            context.Symptoms.AddRange(skinSymptoms);
            context.Symptoms.AddRange(legsSymptoms);
            context.Symptoms.AddRange(noseAndMouthSymptoms);
            context.Symptoms.AddRange(mainSymptoms);
            context.Symptoms.AddRange(pelvicOrgansSymptoms);
            context.Symptoms.AddRange(groinSymptoms);
            context.Symptoms.AddRange(handsSymptoms);
            context.Symptoms.AddRange(backSymptoms);
            context.Symptoms.AddRange(earsSymptoms);
            context.Symptoms.AddRange(neckSymptoms);
            context.Symptoms.AddRange(buttocksSymptoms);

            #endregion

            #region Diagnoses

            List<Diagnosis> diagnoses = new List<Diagnosis>() {
                new Diagnosis(){ Name="Аллергия" },
                new Diagnosis(){ Name="Аневризма" },
                new Diagnosis(){ Name="Аритмия сердца" },
                new Diagnosis(){ Name="Аднексит" },
                new Diagnosis(){ Name="Атрофический ринит" },
                new Diagnosis(){ Name="Бешенство" },
                new Diagnosis(){ Name="Болезнь Бехтерева" },
                new Diagnosis(){ Name="Ветрянка" },
                new Diagnosis(){ Name="Витилиго" },
                new Diagnosis(){ Name="Водянка яичка" },
                new Diagnosis(){ Name="Воспаление крайней плоти" },
                new Diagnosis(){ Name="Гайморит" },
                new Diagnosis(){ Name="Гастрит желудка" },
                new Diagnosis(){ Name="Гемисинусит" },
                new Diagnosis(){ Name="Геморрагический инсульт головного мозга" },
                new Diagnosis(){ Name="Герпес" },
                new Diagnosis(){ Name="Гестационный сахарный диабет" },
                new Diagnosis(){ Name="Гидронефроз" },
                new Diagnosis(){ Name="Гидроцефалия" },
                new Diagnosis(){ Name="Гипогликемия" },
                new Diagnosis(){ Name="Гипокалиемия" },
                new Diagnosis(){ Name="Гонорея" },
                new Diagnosis(){ Name="Грипп" },
                new Diagnosis(){ Name="Дальтонизм" },
                new Diagnosis(){ Name="Демодекоз" },
                new Diagnosis(){ Name="Дисбактериоз кишечника" },
                new Diagnosis(){ Name="Дифтерия" },
                new Diagnosis(){ Name="Дуоденит" },
                new Diagnosis(){ Name="Желтая лихорадка" },
                new Diagnosis(){ Name="Инсульт" },
                new Diagnosis(){ Name="Инфаркт миокарда" },
                new Diagnosis(){ Name="Инфекционный мононуклеоз" },
                new Diagnosis(){ Name="Иридоциклит" },
                new Diagnosis(){ Name="Ишиас" },
                new Diagnosis(){ Name="Ишемический инсульт головного мозга" },
                new Diagnosis(){ Name="Кандидоз" },
                new Diagnosis(){ Name="Кариес зубов" },
                new Diagnosis(){ Name="Киста яичника" },
                new Diagnosis(){ Name="Кифоз грудного отдела позвоночника" },
                new Diagnosis(){ Name="Корь" },
                new Diagnosis(){ Name="Косоглазие" },
                new Diagnosis(){ Name="Крапивница" },
                new Diagnosis(){ Name="Микоплазмоз" },
                new Diagnosis(){ Name="Миокардиодистрофия" },
                new Diagnosis(){ Name="Миома матки" },
                new Diagnosis(){ Name="Нарушение терморегуляции организма" },
                new Diagnosis(){ Name="Одонтогенный гайморит" },
                new Diagnosis(){ Name="Остеохондроз грудного отдела позвоночника" },
                new Diagnosis(){ Name="Остеохондроз поясничного отдела позвоночника" },
                new Diagnosis(){ Name="Остеохондроз шейного отдела позвоночника" },
                new Diagnosis(){ Name="Острый аппендицит" },
                new Diagnosis(){ Name="Острый бронхит" },
                new Diagnosis(){ Name="Острый аппендицит" },
                new Diagnosis(){ Name="Отек Квинке" },
                new Diagnosis(){ Name="Пансинусит" },
                new Diagnosis(){ Name="Почечная недостаточность" },
                new Diagnosis(){ Name="Рак почки" },
                new Diagnosis(){ Name="Расстройства личности" },
                new Diagnosis(){ Name="Растяжение связок голеностопного сустава" },
                new Diagnosis(){ Name="Риносинусит" },
                new Diagnosis(){ Name="Саркоидоз" },
                new Diagnosis(){ Name="Сахарный диабет 1-го типа" },
                new Diagnosis(){ Name="Сахарный диабет 2-го типа" },
                new Diagnosis(){ Name="Сердечная недостаточность" },
                new Diagnosis(){ Name="Синусит" },
                new Diagnosis(){ Name="Спайки" },
                new Diagnosis(){ Name="Стрептодермия" },
                new Diagnosis(){ Name="Уреаплазмоз" },
                new Diagnosis(){ Name="Фарингит" },
                new Diagnosis(){ Name="Фимоз" },
                new Diagnosis(){ Name="Острый аппендицит" },
                new Diagnosis(){ Name="Халязион века" },
                new Diagnosis(){ Name="Хеликобактер пилори" },
                new Diagnosis(){ Name="Хламидиоз" },
                new Diagnosis(){ Name="Холера" },
                new Diagnosis(){ Name="Хронический бронхит" },
                new Diagnosis(){ Name="Цистит" }
            };

            context.Diagnoses.AddRange(diagnoses);

            #endregion

        }
    }
}