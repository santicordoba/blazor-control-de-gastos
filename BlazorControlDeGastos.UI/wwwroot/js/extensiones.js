function DescargarArchivo (nombreArchivo, base64) {
    const link = document.createElement('a');
    link.href = "data:application/vnd.ms-excel;base64," + base64;
    link.download = nombreArchivo;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}