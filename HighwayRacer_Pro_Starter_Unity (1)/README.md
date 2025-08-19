# Highway Racer – **Pro Starter (Unity)**

این یک استارتر حرفه‌ای برای پروژه‌ایه که گفتی: گاراژ واقعی با دیوارهای آبی، انتخاب ماشین، تغییر زنده ارتفاع/رینگ/اگزوز،
کنترل کامل (گاز/ترمز/کلاچ/دنده/فرمان)، کیلومترشمار 0–300، پنوماتیک (1/2/3)، و ترافیک *سه مدل* با سرعت ثابت **۲۰km/h**.

> **Engine پیشنهاد:** Unity 2021 LTS یا بالاتر (ترجیحاً 2022/2023) + URP.  
> **Platform:** WebGL (برای لینک عمومی) + Standalone (برای کیفیت بالاتر).

---

## ساختار پوشه
```
Assets/
  Scripts/
    AirSuspension.cs
    GameManager.cs
    GarageUI.cs
    SaveLoad.cs
    SpeedoTachoUI.cs
    TrafficController.cs
    VehicleController.cs
    WeatherTimeManager.cs
  UI/
    SpeedoTachoCanvas.prefab (اینجا فقط اسکریپت‌هاست؛ Prefab را داخل ادیتور بساز)
README.md
```

---

## راه‌اندازی سریع (۵ دقیقه)

1) Unity Hub → **New Project** → Template: **3D (URP)** → Name: `HighwayRacerPro`  
2) بعد از ساخت پروژه، این ZIP را **Extract** کن و محتویات `Assets/` را داخل `Assets/` پروژه کپی کن.  
3) **Sceneها** را بساز:
   - **Garage.unity** (گاراژ با دیوارهای آبی):  
     - یک **Room** بساز: ۳ تا `Cube` (دیوار چپ/راست/پشت) + `Plane` (کف).  
       - رنگ دیوارها: آبی (`#1e3a8a`)، کف: خاکستری تیره.  
     - یک **Directional Light** نرم.  
     - یک خودرو `Car_Garage` (Prefab از مدل بدون لوگو؛ موقتاً یک `Cube` به‌عنوان بدنه + 4 `Cylinder` برای چرخ) بساز.  
     - یک Canvas **UI** بساز و اسکریپت `GarageUI` را روی یک GameObject خالی به اسم `GarageManager` بچسبان.
       - Dropdown انتخاب خودرو، Dropdown رینگ، Dropdown اگزوز، Slider ارتفاع جلو/عقب، دکمه **Start**.
   - **Highway.unity** (بازی):  
     - یک جاده ساده با تکرار `Plane/Cube` (یا از ProBuilder).  
     - یک خودرو `Car_Player` با **WheelCollider**‌ها (۴ تا) + اسکریپت `VehicleController`.  
     - یک Canvas **HUD** بساز: سرعت‌سنج/تاکومتر (اسکریپت `SpeedoTachoUI`).  
     - `TrafficController` را اضافه کن (۳ مدل ماشین ساده با سرعت ۲۰km/h).  
     - `WeatherTimeManager` را اضافه کن (روز/شب/باران/مه).  
     - `GameManager` را اضافه کن (Load تنظیمات از گاراژ).

4) **ورودی‌ها**  
   - گاز/ترمز: W/S  
   - فرمان: ← / →  
   - کلاچ: Q  
   - دنده بالا/پایین: D / A  
   - پنوماتیک: 1 (جلو پایین) / 2 (همه بالا) / 3 (کف‌خواب)

5) **Build → WebGL**  
   - `File → Build Settings → WebGL → Switch Platform → Build`.  
   - `Compression`: `Gzip` یا `Brotli`.  
   - `Data Caching`: روشن.  
   - Player Settings → Publishing Settings → **Decompression Fallback**: روشن (برای Netlify/Cloudflare راحت‌تر می‌شود).
   - سپس خروجی را در یک پوشه مثل `Build/WebGL` بساز.

---

## هاست رایگان حرفه‌ای (برای لینک عمومی)

- **Unity Play** (آسان‌ترین، رسمی): از داخل ادیتور `Publish → WebGL` → لینک عمومی می‌دهد.  
- **itch.io** (HTML5): یک *ZIP* از پوشه Build بساز، در صفحه بازی **Upload files → This file will be played in the browser** را فعال کن.  
- **SIMMER.io** (ویژه Unity WebGL): ثبت‌نام → Drag & Drop پوشه Build.  
- **Netlify**: https://app.netlify.com/drop  → کل پوشه Build/WebGL را بکش و رها کن.  
- **Cloudflare Pages**: ریپو گیت‌هاب (build آماده) → `Framework preset: None` → مسیر `/` → Deploy.

> برای Netlify/Cloudflare اگر فشرده‌سازی Brotli/Gzip فعال است، **Decompression Fallback** را در Player Settings روشن بگذار تا بدون کانفیگ هدرها هم کار کند.

---

## نکته درباره «واقعی بودن»
- ماشین‌ها در این استارتر **بدون لوگو** هستند؛ برای شباهت نزدیک، مدل‌های *brandless but similar* (FBX/GLB) را وارد کن و در Prefab `Car_Player` جایگزین کن.
- اسکریپت‌ها تنظیمات (شتاب/نسبت دنده/چسبندگی/ترمز) را **public** گذاشته‌اند تا مثل NFS رگلاژ کنی.
- سرعت‌سنج از ۰ تا ۳۰۰ به‌صورت **فیزیکی (m/s → km/h)** محاسبه می‌شود.

---

## کار بعدی
1) مدل‌های واقعی‌تر را وارد کن (بدون لوگو).  
2) Prefab رینگ و اگزوز را بساز و به `GarageUI` معرفی کن تا هنگام انتخاب، **در لحظه روی خودرو** اعمال شود.  
3) کیفیت سایه/AA/URP Volume را با توجه به پرفورمنس مرورگر تنظیم کن (۹۰fps هدف؛ روی PC های قوی achievable).

موفق باشی 🚗💨
