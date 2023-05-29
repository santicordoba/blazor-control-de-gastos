# Control de Gastos - Documentación del Proyecto 📄

## Estructura del Proyecto 🗃

El proyecto está compuesto por los siguientes componentes:

- **API**: Una API desarrollada en .NET Core 6. ⚙
- **Sitio web Blazor**: Una aplicación web desarrollada en Blazor. 💻
- **BOT de Telegram**: Un BOT para Telegram desarrollado en .NET. 🤖

Tanto el sitio web como el bot hacen uso de la API para comunicarse con la base de datos SQL Server.

## Descripción 👨‍💻

Control de Gastos es una aplicación web que permite gestionar los ingresos y gastos de un usuario, con el objetivo de hacer un seguimiento detallado de los movimientos financieros. La aplicación cuenta con las siguientes características:

### Sección de Categorías 📑

En esta sección, los usuarios pueden crear y editar categorías relacionadas con sus ingresos y gastos. Esto ayuda a organizar y clasificar de manera efectiva los movimientos financieros.

### Registro de Gastos e Ingresos 💵

La aplicación ofrece un formulario donde los usuarios pueden ingresar sus gastos e ingresos de forma sencilla. Además, se proporciona una tabla histórica que muestra los registros anteriores de gastos e ingresos. La tabla se puede ordenar haciendo clic en los encabezados de las columnas y también se puede exportar a un archivo .XLS para su apertura en Excel u otros programas similares.

### Edición y Eliminación de Movimientos 🖥 

Al hacer clic en un elemento de la tabla histórica de gastos e ingresos, se muestra un formulario que permite visualizar y editar los valores registrados. También es posible eliminar un registro si es necesario.

### Dashboard 📊

La página de inicio del sitio web cuenta con un dashboard que presenta un gráfico interactivo para visualizar los gastos e ingresos divididos por categoría. Además, se muestra el balance actual y los últimos movimientos registrados. El dashboard se puede actualizar sin realizar una recarga completa de la página mediante un botón específico.

### Bot de Telegram 🤖

El proyecto incluye un bot de Telegram que permite registrar gastos o ingresos, consultar movimientos y balances. El bot está programado en .NET utilizando el paquete NuGet Telegram.Bot. Es muy intuitivo de usar, ya que la navegación se realiza a través de botones interactivos.

El bot se encuentra conectado a la API, lo que garantiza que cualquier registro realizado a través del bot se refleje tanto en el sitio web como en la aplicación de Telegram.

Espero que esta documentación sea de ayuda para comprender la estructura y funcionalidades del proyecto "Control de Gastos". Si tienes alguna pregunta adicional, no dudes en hacerla. santicordoba93@gmail.com ✉
