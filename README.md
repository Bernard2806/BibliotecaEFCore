# 📚 BibliotecaEFCore

Este proyecto es una aplicación de consola en C# que demuestra cómo utilizar **Entity Framework Core** con el enfoque **Code First** y una arquitectura basada en **servicios**. Permite gestionar una biblioteca con autores, libros, usuarios y préstamos.

## 🧠 ¿Qué muestra este proyecto?

- Uso de Entity Framework Core con relaciones entre entidades
- Creación automática de la base de datos
- Operaciones CRUD organizadas por servicios
- Relación uno a muchos y muchos a muchos
- Buenas prácticas en proyectos de consola .NET

## 🧱 Entidades

- **Autor**: representa un autor y puede tener muchos libros.
- **Libro**: pertenece a un autor y puede ser prestado a un usuario.
- **Usuario**: representa a un lector que puede tomar libros prestados.
- **Préstamo**: representa el préstamo de un libro a un usuario.

## 🔧 Cómo ejecutar

1. Cloná el repositorio:

```bash
git clone https://github.com/tu-usuario/BibliotecaEFCore.git
cd BibliotecaEFCore
```

2. Restaurá los paquetes:

```bash
dotnet restore
```

3. Ejecutá la app:

```bash
dotnet run
```

## 🛠️ Tecnologías

- .NET 8
- Entity Framework Core
- SQLite
