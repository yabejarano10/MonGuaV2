# MonGuaV2

El sistema utiliza 2 proyectos: MonGua y DataContext por lo cual clonar el repositorio con ambos proyectos en la misma carpeta.

## Carga Masiva
El sistema permite la carga masiva de Pacientes y Consultorios a través de archivos separados por comas (.csv) con los nombres "Patients.csv" y "Consultories.csv".

* Al interior de la ruta: "/MonGua/bin/Debug/netcoreapp3.1" se encuentran 2 archivos que ya tienen algunos datos, con la estructura que se muestra a continuación:

  * Patients.csv<br/>
  ![Patients](https://i.imgur.com/RFdm73t.png)
  
  * Consultories.csv<br/>
  ![Consultories](https://i.imgur.com/OJbeaez.png)
  
  Pueden hacer uso de estos archivos o crear unos propios siempre y cuando tengan la misma estructura y se encuentren en la misma ruta: "/MonGua/bin/Debug/netcoreapp3.1" y 
  se utilicen los mismos nombres de archivo.
 * Para los Patients, todas las variables son arbitrarias excepto el ID que debe ser incremental como se muestra en la figura ya que
  se usa para parte del ordenamiento.
 * Revisar también el formato del archivo DNA.txt para asegurar que no hayan fallos al cargar un archivo diferente para la prueba de ADN.
  
