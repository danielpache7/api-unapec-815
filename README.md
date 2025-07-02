# Sistema de Cheques - API RESTful

API RESTful desarrollada en .NET 8 para el Sistema de Cheques siguiendo los principios de Clean Architecture.

## 🏗️ Arquitectura

El proyecto implementa Clean Architecture con las siguientes capas:

- **SistemaCheques.API**: Capa de presentación con controladores REST
- **SistemaCheques.Application**: Lógica de negocio, comandos y queries (CQRS con MediatR)
- **SistemaCheques.Domain**: Entidades, interfaces y lógica de dominio
- **SistemaCheques.Infrastructure**: Implementación de repositorios y acceso a datos
- **SistemaCheques.Tests**: Pruebas unitarias y de integración

## 🛠️ Tecnologías Utilizadas

- **.NET 8**: Framework principal
- **ASP.NET Core Web API**: Para crear la API REST
- **Entity Framework Core**: ORM para acceso a datos
- **PostgreSQL**: Base de datos relacional
- **MediatR**: Implementación del patrón CQRS
- **JWT Bearer**: Autenticación y autorización
- **Swagger/OpenAPI**: Documentación automática de la API
- **xUnit**: Framework de pruebas
- **FluentAssertions**: Assertions expresivas para pruebas

## 📋 Entidades Principales

### ConceptoPago
- Gestión de conceptos de pago
- Estados activo/inactivo

### Proveedor
- Información de proveedores (persona física/jurídica)
- Balance y cuentas contables
- Validación única de cédula/RNC

### SolicitudCheque
- Solicitudes de cheques por proveedor
- Estados: Pendiente, Anulada, Cheque Generado
- Integración con cuentas contables

### AsientoContable
- Generación automática de asientos contables
- Integración con sistema de contabilidad
- Agrupación por período y cuenta

## 🚀 Endpoints Principales

### Conceptos de Pago
```
GET /api/conceptospago - Listar conceptos
GET /api/conceptospago/{id} - Obtener concepto por ID
POST /api/conceptospago - Crear concepto
PUT /api/conceptospago/{id} - Actualizar concepto
DELETE /api/conceptospago/{id} - Eliminar concepto
```

### Proveedores
```
GET /api/proveedores - Listar proveedores
GET /api/proveedores/{id} - Obtener proveedor por ID
POST /api/proveedores - Crear proveedor
PUT /api/proveedores/{id} - Actualizar proveedor
DELETE /api/proveedores/{id} - Eliminar proveedor
```

### Solicitudes de Cheque
```
GET /api/solicitudes - Listar solicitudes
POST /api/solicitudes - Crear solicitud
POST /api/solicitudes/{id}/generar-cheque - Generar cheque
POST /api/solicitudes/{id}/anular - Anular solicitud
```

### Asientos Contables
```
GET /api/asientos - Listar asientos
POST /api/asientos/generar - Generar asiento del mes
POST /api/asientos/{id}/enviar - Enviar al sistema contable
```

## ⚙️ Configuración

### 1. Requisitos Previos
- .NET 8 SDK
- PostgreSQL 12+
- Visual Studio 2022 / VS Code

### 2. Base de Datos
Actualizar la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=SistemaCheques;Username=tu_usuario;Password=tu_password"
  }
}
```

### 3. Instalación y Ejecución
```bash
# Clonar el repositorio
git clone [url-repositorio]
cd SistemaCheques

# Restaurar paquetes
dotnet restore

# Crear y aplicar migraciones
dotnet ef migrations add InitialCreate --project SistemaCheques.Infrastructure --startup-project SistemaCheques.API

# Ejecutar la aplicación
dotnet run --project SistemaCheques.API
```

## 🧪 Pruebas

### Ejecutar Pruebas Unitarias
```bash
dotnet test SistemaCheques.Tests
```

## 📚 Documentación API

Una vez ejecutada la aplicación, acceder a:
- **Swagger UI**: `https://localhost:7000` (puerto por defecto)

## 🔐 Seguridad

- **Autenticación**: JWT Bearer tokens
- **Autorización**: Basada en roles y permisos
- **Validación**: Validación de datos en todos los endpoints
- **CORS**: Configurado para orígenes específicos

## 🚀 Funcionalidades Implementadas

### Patrón CQRS
Separación clara entre comandos (escritura) y queries (lectura) usando MediatR.

### Unit of Work
Gestión de transacciones y consistencia de datos.

### Repository Pattern
Abstracción del acceso a datos con implementaciones específicas.

### Mapeo Automático
Conversión entre entidades y DTOs mediante extension methods.

### Logging
Sistema de logging configurable para auditoría y debugging.

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. 