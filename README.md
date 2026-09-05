# 📚 WebApplication2 — Sistema de Biblioteca

Aplicación web desarrollada con **ASP.NET Core MVC (.NET 10)** que gestiona un sistema de biblioteca. Permite administrar autores, categorías, libros, usuarios y préstamos, con persistencia en base de datos **MySQL** mediante **Entity Framework Core**.

---

## 🗂️ Estructura del Proyecto

```
WebApplication2/
├── Controllers/
│   ├── AutoresController.cs
│   ├── CategoriasController.cs
│   ├── HomeController.cs
│   ├── LibrosController.cs
│   ├── PrestamosController.cs
│   └── UsuariosController.cs
├── Data/
│   ├── ApplicationDbContext.cs   # Contexto de EF Core
│   └── SeedData.cs               # Datos iniciales (seed)
├── Models/
│   ├── Autor.cs
│   ├── Categoria.cs
│   ├── Libro.cs
│   ├── Prestamo.cs
│   ├── TarjetaBiblioteca.cs
│   └── Usuario.cs
├── Migrations/                   # Migraciones de EF Core
├── Views/                        # Vistas Razor
├── appsettings.json              # Configuración de la app
└── Program.cs                    # Punto de entrada
```

---

## ✅ Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

| Herramienta | Versión recomendada | Enlace |
|---|---|---|
| .NET SDK | 10.0 o superior | https://dotnet.microsoft.com/download |
| MySQL Server | 8.x | https://dev.mysql.com/downloads/ |
| Visual Studio | 2022 (v17.x) o VS Code | https://visualstudio.microsoft.com/ |

---

## ⚙️ Configuración

### 1. Clonar o abrir el proyecto

Abre la solución `WebApplication2.slnx` en Visual Studio, o navega a la carpeta del proyecto en una terminal:

```bash
cd d:\.netEjercicios\WebApplication2
```

### 2. Configurar la cadena de conexión

Edita el archivo `appsettings.json` y ajusta los valores de conexión a tu instancia de MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=WebApplication1;User=root;Password=root;"
  }
}
```

> **⚠️ Importante:** Cambia `User` y `Password` por las credenciales de tu servidor MySQL. En entornos de producción, nunca expongas credenciales en texto plano; usa variables de entorno o el gestor de secretos de .NET.

### 3. Crear la base de datos

Asegúrate de que MySQL esté corriendo. Luego aplica las migraciones de Entity Framework para crear el esquema:

```bash
dotnet ef database update
```

> Si no tienes la herramienta `dotnet-ef` instalada globalmente, instálala primero:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

## ▶️ Ejecución

### Desde Visual Studio

1. Abre `WebApplication2.slnx`.
2. Presiona **F5** (con depuración) o **Ctrl+F5** (sin depuración).
3. La aplicación se abrirá automáticamente en el navegador.

### Desde la terminal (CLI)

```bash
dotnet run
```

La aplicación estará disponible por defecto en:

- `https://localhost:7xxx` (HTTPS)
- `http://localhost:5xxx` (HTTP)

> Los puertos exactos se muestran en la consola al iniciar la aplicación.

---

## 🌱 Datos Iniciales (Seed)

Al iniciar la aplicación por primera vez, se ejecuta automáticamente `SeedData.Poblar()` que carga datos de ejemplo en la base de datos. No es necesario ningún paso adicional.

---

## 📦 Dependencias NuGet

| Paquete | Versión |
|---|---|
| `Microsoft.EntityFrameworkCore` | 10.0.11 |
| `Microsoft.EntityFrameworkCore.Design` | 10.0.11 |
| `MySql.EntityFrameworkCore` | 10.0.9 |

Para restaurar los paquetes manualmente:

```bash
dotnet restore
```

---

## 🧱 Módulos del Sistema

| Módulo | Descripción |
|---|---|
| **Autores** | CRUD completo de autores de libros |
| **Categorías** | Gestión de categorías literarias |
| **Libros** | Registro de libros con autor y categoría |
| **Usuarios** | Administración de usuarios de la biblioteca |
| **Préstamos** | Control de préstamos y devoluciones |

---

## 🛠️ Comandos Útiles de EF Core

```bash
# Crear una nueva migración
dotnet ef migrations add <NombreMigracion>

# Aplicar migraciones pendientes
dotnet ef database update

# Revertir la última migración
dotnet ef migrations remove

# Ver el estado de las migraciones
dotnet ef migrations list
```

---

## 📝 Notas

- El proyecto usa el patrón **MVC** (Model-View-Controller).
- La base de datos se llama `WebApplication1` por defecto (definida en `appsettings.json`).
- El entorno de desarrollo tiene configuración adicional en `appsettings.Development.json`.
