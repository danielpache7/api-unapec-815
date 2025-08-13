# Requisitos Funcionales – API (.NET Backend)

Este documento resume los requisitos funcionales que debe cumplir el backend del Sistema de Cheques.

---

### 1. Gestión de Conceptos de Pago
- CRUD de conceptos.
- Campos mínimos:
  - Identificador
  - Descripción
  - Estado
- ✅ Endpoint sugerido: `/api/conceptos`
- ℹ️ Si ya está implementado de otra forma, se acepta mientras cubra la funcionalidad.

---

### 2. Gestión de Proveedores
- CRUD de proveedores.
- Campos mínimos:
  - Identificador
  - Nombre
  - Tipo de Persona (Física / Jurídica)
  - Cédula / RNC
  - Balance
  - Cuenta Contable Proveedor
  - Estado
- ✅ Endpoint sugerido: `/api/proveedores`

---

### 3. Registro de Solicitudes de Cheques
- Creación de solicitudes con los siguientes campos:
  - Número de Solicitud
  - Proveedor
  - Monto
  - Fecha de Registro
  - Estado (Pendiente / Anulada / Cheque Generado)
  - Cuenta Contable Proveedor
  - Cuenta Contable Bancaria
- ✅ Endpoint sugerido: `/api/solicitudes-cheques`

---

### 4. Generación de Cheques
- Selección de solicitudes pendientes para:
  - Generar cheque (agrega número y cambia estado)
  - Anular solicitud (cambia estado)
- ✅ Endpoint sugerido: `/api/cheques/generar`

---

### 5. Registro de Asiento Contable
- Desde un listado de cheques del mes.
- Agrupa montos por cuenta y los envía a contabilidad.
- Integra con servicio web externo.
- ✅ Endpoint sugerido: `/api/asientos/registrar`

---

### 6. Consultas por Criterios
- Buscar cheques por:
  - Proveedor
  - Rango de fechas
  - Estado
- ✅ Endpoint sugerido: `/api/cheques/consulta`

---

### 7. Control de Acceso
- Autenticación y autorización.
- Manejo de roles o permisos.

---

## 🟡 Notas
- Los endpoints mencionados son sugerencias. Si ya están implementados con otra ruta o estructura, se acepta mientras cumplan la lógica.