USE [inclub]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 26/08/2022 20:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DniCliente] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 26/08/2022 20:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Precio] [numeric](10, 2) NULL,
	[Descripcion] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/08/2022 20:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](100) NOT NULL,
	[Apellidos] [varchar](100) NULL,
	[NombreUsuario] [varchar](100) NOT NULL,
	[Pass] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([id])
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([id])
GO
/****** Object:  StoredProcedure [dbo].[ListarCompraUsuario]    Script Date: 26/08/2022 20:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListarCompraUsuario]
	@idUsuario int = null
 AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
DniCliente,
IdUsuario,
IdProducto,
Fecha,
u.Nombres as NombresCliente,
u.Apellidos as ApellidosCliente,
p.Nombre as NombreProducto,
p.Precio as PrecioProducto
FROM
	Compras c
INNER JOIN Usuarios u
	ON c.IdUsuario = u.id
INNER JOIN Productos p
	ON c.IdProducto = p.id
WHERE @idUsuario IS NULL OR  u.id = @idUsuario
ORDER BY
	Fecha DESC
END
GO
/****** Object:  StoredProcedure [dbo].[SelectUsuarios]    Script Date: 26/08/2022 20:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SelectUsuarios]

  @id int

 as

 Begin

  select *

   from Usuarios

   where id= @id;

   END

GO
