# 火車訂票應用程式

> 資料庫程式設計與管理期末專題 — 使用 C# Windows Forms 開發的完整火車票訂購管理系統，提供使用者註冊、登入、訂票、查詢班次、票務管理等功能。

## 目錄

- [專案簡介](#專案簡介)
- [系統功能](#系統功能)
- [技術架構](#技術架構)
- [系統需求](#系統需求)
- [資料庫設定](#資料庫設定)
- [安裝與執行](#安裝與執行)
- [專案結構](#專案結構)
- [票價計算規則](#票價計算規則)
- [授權](#授權)

---

## 專案簡介

Railway Booking System 是一個功能完整的火車訂票管理系統，支援以下核心功能：

- 安全的使用者認證系統（SHA256 密碼加密）
- 即時班次查詢與座位狀態
- 線上訂票與座位預訂
- 電子車票列印功能
- 會員點數累積制度
- 訂單管理與查詢

---

## 系統功能

### 使用者認證

- **登入系統** - 使用 Email 與密碼登入，採用 SHA256 + Salt 加密儲存；驗證成功後進入大廳
- **密碼重設** - 支援修改密碼功能，包含密碼確認驗證

### 訂票功能

- **班次查詢** - 根據起訖站與日期時間搜尋可用班次
  - 選擇起訖站（ComboBox）、乘車日期與時間（DateTimePicker）
  - 查詢可用班次並顯示車次編號、車種類型、發車/抵達時間
- **即時座位** - 顯示每個班次的剩餘座位數
- **票價計算** - 依據里程數自動計算票價，並顯示總里程數與可獲得會員點數
- **訂單建立** - 完成訂票並分配座位

### 票務管理

- **個人車票** - 查看所有訂單記錄（未付款、已付款、已取消）
  - ListView 顯示訂單編號、車次、車種、乘車日期、起終點站、上下車時間、座位、金額、付款方式、訂購/付款/取消時間
- **車票列印** - 支援電子車票列印預覽
  - 自訂票券尺寸（300x500）、載入車票背景圖
- **訂單查詢** - 完整的訂單資訊顯示

### 班次資訊

- **列車狀態** - 查詢指定路線的列車時刻表
  - 顯示車次與車種、起發站/到達站、出發/抵達時間、行駛時間（分鐘）
- **即時狀態** - 顯示列車狀態（準點、誤點、取消）

### 個人資訊

- **會員資料** - 顯示電子郵件、註冊日期
- **會員點數** - 累積消費點數顯示
- 採用 TableLayoutPanel 動態佈局設計

### 系統公告

- **動態公告** - 首頁顯示系統最新公告、系統公告動態載入
- **功能導航** - 大廳首頁顯示歡迎訊息、功能選單導航

---

## 技術架構

### 開發框架

- **.NET 8.0** - Windows Desktop Runtime
- **Windows Forms** - 桌面應用程式介面框架
- **C# 12** - 程式語言

### 資料庫

- **SQL Server** - 關聯式資料庫管理系統
- **System.Data.SqlClient** - ADO.NET 資料存取
- **Stored Procedures** - 預存程序 (CreateBooking, FindAvailableTrips, GetTotalTravelDistance)

### 安全性

- **SHA256** - 密碼雜湊演算法
- **Salt** - 密碼加鹽儲存
- **參數化查詢** - 防止 SQL Injection

---

## 系統需求

### 開發環境

- **作業系統**: Windows 10/11
- **開發工具**: Visual Studio 2022 (或更新版本)
- **.NET SDK**: 8.0 或更新版本
- **SQL Server**: 2019 或更新版本

### 執行環境

- **作業系統**: Windows 10/11
- **.NET Runtime**: 8.0-windows Desktop Runtime
- **網路連線**: 需連線至 SQL Server 資料庫

---

## 資料庫設定

### 連線字串設定

預設連線字串位於 `Global.cs` 檔案：

```csharp
public static string conn_str { get; } = @"Data Source=26.107.110.158\SQL2022_1141; 
                                           Integrated Security=false;
                                           user=sqluser;
                                           password=123; 
                                           Initial Catalog=BookTrainTickets";
```

### 資料庫架構

`BookTrainTickets.bak` 為所使用的資料庫範例備份檔。

### 必要的預存程序

- `CreateBooking` - 建立訂單
- `FindAvailableTrips` - 查詢可用班次
- `GetTotalTravelDistance` - 計算總里程

---

## 安裝與執行

### 1. Clone 專案

```bash
git clone <repository-url>
cd RailwayBooking
```

### 2. 開啟專案

使用 Visual Studio 開啟 `RailwayBooking.sln`

### 3. 還原 NuGet 套件

系統會自動還原以下套件：

- System.Data.SqlClient (v4.9.0)

或手動執行：

```bash
dotnet restore
```

### 4. 設定資料庫連線

修改 `Global.cs` 中的連線字串：

```csharp
public static string conn_str { get; } = @"Data Source=YOUR_SERVER;
                                           Integrated Security=false;
                                           user=YOUR_USER;
                                           password=YOUR_PASSWORD;
                                           Initial Catalog=BookTrainTickets";
```

### 5. 執行程式

- 按 `F5` 或點擊「開始偵錯」
- 或使用指令：

  ```bash
  dotnet run
  ```

---

## 專案結構

```
RailwayBooking/
│
├── RailwayBooking.sln              # Visual Studio 方案檔
├── README.md                       # 專案說明文件
│
└── RailwayBooking/                 # 主專案目錄
    │
    ├── Program.cs                  # 程式進入點
    ├── Global.cs                   # 全域變數與設定
    ├── lib.cs                      # 工具類別（票價、點數計算）
    │
    ├── Forms/                      # 表單檔案
    │   ├── LoginForm.cs            # 登入表單
    │   ├── LobbyForm.cs            # 大廳/首頁
    │   ├── BookingTicketsForm.cs   # 訂票表單
    │   ├── PersonalTrainTicketForm.cs  # 個人車票
    │   ├── TrainStatusForm.cs      # 列車狀態查詢
    │   ├── PersonalInformationForm.cs  # 個人資訊
    │   ├── ResetPasswordForm.cs    # 重設密碼
    │   └── AnnouncementItem.cs     # 公告項目控制項
    │
    ├── Images/                     # 圖片資源
    │   └── ticket_bg.png           # 車票背景圖
    │
    └── bin/Debug/net8.0-windows/   # 編譯輸出目錄
```

---

## 票價計算規則

系統採用**階梯式里程計費**，票價隨著距離遞減：

| 里程區間 | 每公里單價 |
|---------|-----------|
| 0-50 km | NT$ 3.39 |
| 51-100 km | NT$ 2.92 |
| 101-200 km | NT$ 2.81 |
| 201-300 km | NT$ 2.37 |
| 300+ km | NT$ 2.20 |

### 計算邏輯

```csharp
public static int CalculateFare(double km)
{
    double[] thresholds = { 50, 100, 200, 300 };
    double[] rates = { 3.39, 2.92, 2.81, 2.37, 2.20 };
    
    // 分段計算票價
    // ...
    
    return (int)Math.Round(fare, MidpointRounding.AwayFromZero);
}
```

### 會員點數

- 每消費 **NT$ 50** 可獲得 **1 點**會員點數

```csharp
public static int CalculateMemberPoint(int money)
{
    return (int)money / 50;
}
```

---

## 授權

此專案為期末專題，以學習用途開發。
