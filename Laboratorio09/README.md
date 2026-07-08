# LABORATORIO: Clasificación y Reconocimiento
## Resources 
- opencv-python==4.10.0.84
- numpy==1.26.4
- tensorflow==2.16.1
- ultralytics==8.4.0
- cvzone==2.0.0
- streamlit==1.59.0

# 🔍 Sistema de Clasificación y Reconocimiento de Objetos con YOLOv8
Este repositorio contiene un sistema dual de visión computacional desarrollado en Python utilizando la arquitectura de red neuronal convolucional YOLOv8 (yolov8n.pt). El proyecto fue creado como parte de la asignatura Computación Gráfica, Visión Computacional y Multimedia en la Universidad Nacional de San Agustín.

El sistema procesa imágenes digitales a través de dos entornos diferenciados:

1. Detección en Tiempo Real: Captura el flujo de video analógico de la cámara web para segmentar e interpretar objetos en vivo.

2. Interfaz Gráfica Web (Dashboard): Una aplicación interactiva construida con Streamlit que permite al usuario subir imágenes locales (.jpg, .png) para realizar análisis de reconocimiento bajo demanda.

## 🛠️ Tecnologías y Requisitos
El proyecto está diseñado para ejecutarse con Python 3.10 o superior. Las dependencias clave utilizadas son:

* OpenCV (opencv-python): Gestión y adquisición del flujo de video.

* Ultralytics (ultralytics): Framework encargado de la inferencia y pesos del modelo YOLOv8.

* Streamlit (streamlit): Servidor y renderizado de la interfaz gráfica web.

* CVZone (cvzone): Optimización estética de las cajas de detección y textos en pantalla.

## 🚀 Instalación y Configuración
Sigue estos pasos para configurar el entorno virtual y ejecutar los scripts en tu computadora:

1. Clonar el repositorio o descargar los archivos
Asegúrate de tener los archivos visioncomputacionalcamara.py (cámara web), visioncomputacional.py (interfaz web) y tu archivo de dependencias en una misma carpeta.

2. Crear un entorno virtual (Recomendado)
Para evitar conflictos de versiones con otras librerías globales, crea un entorno virtual limpio en tu terminal:

```
# Crear el entorno virtual
python -m venv venv

# Activar el entorno virtual
# En Windows:
venv\Scripts\activate
# En Linux/macOS:
source venv/bin/activate
```

3. Instalar las dependencias
Instala todas las librerías necesarias con el siguiente comando:

```
pip install opencv-python==4.10.0.84 numpy==1.26.4 tensorflow==2.16.1 ultralytics==8.4.0 cvzone==2.0.0 streamlit==1.59.0
```
Nota: La primera vez que ejecutes cualquiera de los scripts, la librería ultralytics descargará automáticamente el archivo de pesos yolov8n.pt (aprox. 6 MB) en tu directorio raíz. No necesitas descargarlo manualmente.

## 💻 Ejecución de los Scripts
### Opción A: Detección en Vivo (Cámara Web)
Para ejecutar el reconocimiento de objetos en tiempo real con tu cámara integrada o externa, ejecuta:

```
python visioncomputacionalcamara.py
```

### Opción B: Interfaz Gráfica Web (Streamlit)
Para lanzar el panel interactivo en tu navegador web local, ejecuta:

```
streamlit run visioncomputacional.py
```

## 📦 Estructura del Proyecto

```
├── visioncomputacional.py          # Script de la aplicación interactiva web (Streamlit)
├── visioncomputacionalcamara.py    # Script de la detección en tiempo real (Cámara Web)
├── requirements.txt                # Archivo con las dependencias exactas del entorno
└── README.md                       # Guía de documentación del proyecto (Este archivo)
```