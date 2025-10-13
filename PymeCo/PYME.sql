/* ===============================================
   CREACIÓN DE BASE DE DATOS
   =============================================== */
CREATE DATABASE Pyme;
GO
USE Pyme;
GO

/* ===============================================
   TABLA: Categoria
   =============================================== */
GO
CREATE TABLE dbo.Categoria (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categoria PRIMARY KEY (Id),
    CONSTRAINT UQ_Categoria_Nombre UNIQUE (Nombre)
);
GO

/* ===============================================
   TABLA: Producto
   =============================================== */
GO
CREATE TABLE dbo.Producto (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    CategoriaId INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL CONSTRAINT DF_Producto_Precio DEFAULT (0),
    ImpuestoPorc DECIMAL(5,2) NOT NULL CONSTRAINT DF_Producto_ImpuestoPorc DEFAULT (0),
    Stock INT NOT NULL CONSTRAINT DF_Producto_Stock DEFAULT (0),
    ImagenUrl VARCHAR(255) NULL,

    EstadoProducto VARCHAR(15) NOT NULL
        CONSTRAINT DF_Producto_Estado DEFAULT ('Activo'),
        CONSTRAINT CK_Producto_Estado CHECK (EstadoProducto IN ('Activo','Inactivo')),

    CONSTRAINT PK_Producto PRIMARY KEY (Id),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (CategoriaId)
        REFERENCES dbo.Categoria(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT CK_Producto_Precio CHECK (Precio >= 0),
    CONSTRAINT CK_Producto_ImpuestoPorc CHECK (ImpuestoPorc BETWEEN 0 AND 100),
    CONSTRAINT CK_Producto_Stock CHECK (Stock >= 0)
);
GO

/* ===============================================
   TABLA: Cliente
   =============================================== */
GO
CREATE TABLE dbo.Cliente (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    Cedula VARCHAR(50) NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    Telefono VARCHAR(50) NULL,
    Direccion VARCHAR(255) NULL,

    CONSTRAINT PK_Cliente PRIMARY KEY (Id),
    CONSTRAINT UQ_Cliente_Cedula UNIQUE (Cedula),
    CONSTRAINT UQ_Cliente_Correo UNIQUE (Correo)
);
GO

/* ===============================================
   TABLA: Usuario  (con EstadoUsuario = 'Activo'/'Inactivo')
   =============================================== */
GO
CREATE TABLE dbo.Usuario (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Rol VARCHAR(50) NOT NULL,

    EstadoUsuario VARCHAR(15) NOT NULL
        CONSTRAINT DF_Usuario_Estado DEFAULT ('Activo'),
        CONSTRAINT CK_Usuario_Estado CHECK (EstadoUsuario IN ('Activo','Inactivo')),

    CONSTRAINT PK_Usuario PRIMARY KEY (Id),
    CONSTRAINT CK_Usuario_Rol CHECK (Rol IN ('Admin','Vendedor','Cliente'))
);
GO

/* ===============================================
   TABLA: Pedido
   =============================================== */
GO
CREATE TABLE dbo.Pedido (
    Id INT IDENTITY(1,1) NOT NULL,
    ClienteId INT NOT NULL,
    UsuarioId INT NOT NULL,
    Fecha DATETIME NOT NULL CONSTRAINT DF_Pedido_Fecha DEFAULT (GETDATE()),
    Subtotal DECIMAL(10,2) NOT NULL CONSTRAINT DF_Pedido_Subtotal DEFAULT (0),
    Impuestos DECIMAL(10,2) NOT NULL CONSTRAINT DF_Pedido_Impuestos DEFAULT (0),
    Total DECIMAL(10,2) NOT NULL CONSTRAINT DF_Pedido_Total DEFAULT (0),
    Estado VARCHAR(50) NOT NULL CONSTRAINT DF_Pedido_Estado DEFAULT ('Pendiente'),

    CONSTRAINT PK_Pedido PRIMARY KEY (Id),
    CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (ClienteId)
        REFERENCES dbo.Cliente(Id)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT FK_Pedido_Usuario FOREIGN KEY (UsuarioId)
        REFERENCES dbo.Usuario(Id)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT CK_Pedido_Total CHECK (Total >= 0),
    CONSTRAINT CK_Pedido_Estado CHECK (Estado IN ('Pendiente','Pagado','Enviado','Cancelado'))
);
GO

/* ===============================================
   TABLA: PedidoDetalle
   =============================================== */
GO
CREATE TABLE dbo.PedidoDetalle (
    Id INT IDENTITY(1,1) NOT NULL,
    PedidoId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnit DECIMAL(10,2) NOT NULL,
    Descuento DECIMAL(5,2) NOT NULL CONSTRAINT DF_PedidoDetalle_Descuento DEFAULT (0),
    ImpuestoPorc DECIMAL(5,2) NOT NULL CONSTRAINT DF_PedidoDetalle_Impuesto DEFAULT (0),
    TotalLinea DECIMAL(10,2) NOT NULL CONSTRAINT DF_PedidoDetalle_Total DEFAULT (0),

    CONSTRAINT PK_PedidoDetalle PRIMARY KEY (Id),
    CONSTRAINT FK_PedidoDetalle_Pedido FOREIGN KEY (PedidoId)
        REFERENCES dbo.Pedido(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_PedidoDetalle_Producto FOREIGN KEY (ProductoId)
        REFERENCES dbo.Producto(Id)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT CK_PedidoDetalle_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_PedidoDetalle_Descuento CHECK (Descuento BETWEEN 0 AND 100),
    CONSTRAINT CK_PedidoDetalle_Impuesto CHECK (ImpuestoPorc BETWEEN 0 AND 100),
    CONSTRAINT CK_PedidoDetalle_Total CHECK (TotalLinea >= 0)
);
GO

/* ===============================================
   ÍNDICES
   =============================================== */
CREATE INDEX IX_Producto_Categoria ON dbo.Producto(CategoriaId);
CREATE INDEX IX_Producto_Nombre   ON dbo.Producto(Nombre);
CREATE INDEX IX_Pedido_Cliente    ON dbo.Pedido(ClienteId);
CREATE INDEX IX_Pedido_Usuario    ON dbo.Pedido(UsuarioId);
CREATE INDEX IX_Pedido_Fecha      ON dbo.Pedido(Fecha);
CREATE INDEX IX_PD_Pedido         ON dbo.PedidoDetalle(PedidoId);
CREATE INDEX IX_PD_Producto       ON dbo.PedidoDetalle(ProductoId);
GO

/* ===============================================
   DATOS DE PRUEBA
   =============================================== */
INSERT INTO dbo.Categoria (Nombre) VALUES ('Bebidas'), ('Snacks');

INSERT INTO dbo.Producto (Nombre, CategoriaId, Precio, ImpuestoPorc, Stock, ImagenUrl, EstadoProducto)
VALUES ('Café Molido 250g', 1, 1500, 13, 50, NULL, 'Activo'),
       ('Galletas Choco',  2,  800, 13,100, NULL, 'Inactivo');

INSERT INTO dbo.Cliente (Nombre, Cedula, Correo, Telefono, Direccion)
VALUES ('Mariano Ulloa', '1-1111-1111', 'mariano@mail.com', '8888-8888', 'San José');

INSERT INTO dbo.Usuario (Nombre, Rol, EstadoUsuario)
VALUES ('Administrador General', 'Admin', 'Activo'),
       ('Vendedor 1', 'Vendedor', 'Inactivo');

INSERT INTO dbo.Pedido (ClienteId, UsuarioId, Estado)
VALUES (1, 1, 'Pendiente');

INSERT INTO dbo.PedidoDetalle (PedidoId, ProductoId, Cantidad, PrecioUnit, Descuento, ImpuestoPorc, TotalLinea)
VALUES (1, 1, 2, 1500, 0, 13, 3380),
       (1, 2, 3,  800, 0, 13, 2712);
GO