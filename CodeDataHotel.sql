-- Tạo cơ sở dữ liệu
CREATE DATABASE HotelManagement;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE HotelManagement;
GO

CREATE TABLE [User] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(255) NOT NULL UNIQUE,
    full_name NVARCHAR(255) NOT NULL,
    phone NVARCHAR(15) NOT NULL UNIQUE,
    email NVARCHAR(255) UNIQUE,
    role NVARCHAR(50) CHECK (role IN ('admin', 'staff')) NOT NULL,
    password_hash NVARCHAR(MAX) NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Customer (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(255) NOT NULL,
    phone NVARCHAR(15) NOT NULL UNIQUE,
    email NVARCHAR(255) UNIQUE,
    address NVARCHAR(MAX),
    identity_card NVARCHAR(20) UNIQUE NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Floor (
    id INT IDENTITY(1,1) PRIMARY KEY,
    max_rooms INT NOT NULL CHECK (max_rooms > 0), -- Số phòng tối đa của tầng
    description NVARCHAR(MAX)
);

CREATE TABLE RoomType (
    id INT IDENTITY(1,1) PRIMARY KEY,
    type_name NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(MAX),
    price_per_night DECIMAL(10,2) NOT NULL
);

CREATE TABLE Room (
    id INT IDENTITY(1,1) PRIMARY KEY,
    room_number NVARCHAR(10) NOT NULL UNIQUE,
    type_id INT NOT NULL,
    floor_id INT NOT NULL,
	price_per_day DECIMAL(10,2) NOT NULL,
	price_per_hour DECIMAL(10,2) NOT NULL,
    status NVARCHAR(50) CHECK (status IN ('available', 'occupied', 'maintenance')) NOT NULL DEFAULT 'available',
    FOREIGN KEY (type_id) REFERENCES RoomType(id) ON DELETE CASCADE,
    FOREIGN KEY (floor_id) REFERENCES Floor(id) ON DELETE CASCADE
);

CREATE TABLE Booking (
    id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    staff_id INT NOT NULL,
    check_in DATE NOT NULL,
    total_price_service DECIMAL(10,2) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES Customer(id) ON DELETE CASCADE,
    FOREIGN KEY (staff_id) REFERENCES [User](id) ON DELETE CASCADE
);

CREATE TABLE BookingDetail (
    id INT IDENTITY(1,1) PRIMARY KEY,
    booking_id INT NOT NULL,
    room_id INT NOT NULL,
    FOREIGN KEY (booking_id) REFERENCES Booking(id) ON DELETE CASCADE,
    FOREIGN KEY (room_id) REFERENCES Room(id) ON DELETE CASCADE
);

CREATE TABLE Invoice (
    id INT IDENTITY(1,1) PRIMARY KEY,
    booking_id INT NOT NULL,
	check_out DATE NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    payment_method NVARCHAR(50) CHECK (payment_method IN ('cash', 'credit_card', 'online')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (booking_id) REFERENCES Booking(id) ON DELETE CASCADE
);

CREATE TABLE Service (
    id INT IDENTITY(1,1) PRIMARY KEY,
    service_name NVARCHAR(255) NOT NULL UNIQUE,
    description NVARCHAR(MAX),
    price DECIMAL(10,2) NOT NULL
);

CREATE TABLE RevenueReport (
    id INT IDENTITY(1,1) PRIMARY KEY,
    report_date DATE NOT NULL,
    total_revenue DECIMAL(15,2) NOT NULL DEFAULT 0
);

-- Thêm tài khoản Admin
INSERT INTO [User] (username, full_name, phone, email, role, password_hash)
VALUES 
('admin1', 'Nguyễn Văn A', '0987654321', 'admin1@example.com', 'admin', 'admin123');

-- Thêm tài khoản Staff
INSERT INTO [User] (username, full_name, phone, email, role, password_hash)
VALUES 
('staff1', 'Trần Thị B', '0912345678', 'staff1@example.com', 'staff', 'staff123'),
('staff2', 'Lê Văn C', '0923456789', 'staff2@example.com', 'staff', 'staff456');

-- Xóa các bảng nếu tồn tại
DROP TABLE IF EXISTS Invoice;
DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS Room;
DROP TABLE IF EXISTS RoomType;
DROP TABLE IF EXISTS Customer;
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS Service;
DROP TABLE IF EXISTS RevenueReport;
DROP TABLE IF EXISTS Floor;

INSERT INTO Customer (full_name, phone, email, address, identity_card)
VALUES 
('Phạm Thị D', '0934567890', 'phamthid@example.com', '123 Đường ABC, Quận 1, TP.HCM', '123456789'),
('Nguyễn Văn E', '0945678901', 'nguyenvane@example.com', '456 Đường XYZ, Quận 3, TP.HCM', '234567890'),
('Trần Thị F', '0956789012', 'tranthif@example.com', '789 Đường DEF, Quận 5, TP.HCM', '345678901');

INSERT INTO Floor (max_rooms, description)
VALUES 
(2, 'Tầng 4'),
(10, 'Tầng 1'),
(10, 'Tầng 2'),
(10, 'Tầng 3');

INSERT INTO RoomType (type_name, description, price_per_night)
VALUES 
('Single', 'Tối đa 2 người', 500000),
('Double', 'Tối đa 4 người', 800000),
('Triple', 'Tối đa 6 người', 1200000);

INSERT INTO Room (room_number, type_id, floor_id, price_per_day, price_per_hour, status)
VALUES 
('101', 1, 1,400000, 500000, 'available'),
('102', 2, 1,300000, 400000, 'occupied'),
('201', 3, 2,400000, 500000, 'maintenance');

