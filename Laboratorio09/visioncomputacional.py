import streamlit as st
from ultralytics import YOLO
import cv2
import numpy as np
from PIL import Image

# Configuración de la página web
st.set_page_config(page_title="Detector de Objetos - Lab 9", layout="centered")

st.title("Clasificación y Reconocimiento de Objetos")
st.write("Sube una imagen para procesarla con el modelo preentrenado YOLOv8.")

# 1. Cargar el modelo preentrenado (Base de conocimientos)
@st.cache_resource
def load_model():
    return YOLO("yolov8n.pt") # Modelo ligero, rápido y eficiente (Yolo v8)

model = load_model()

# 2. Adquisición: Interfaz para subir la imagen
uploaded_file = st.file_uploader("Elige una imagen...", type=["jpg", "jpeg", "png"])

if uploaded_file is not None:
    # Convertir el archivo subido a una imagen de PIL y luego a formato OpenCV (BGR)
    source_image = Image.open(uploaded_file)
    st.image(source_image, caption="Imagen Subida", use_container_width=True)
    
    # Convertir a array de numpy para que OpenCV y YOLO lo procesen
    opencv_image = cv2.cvtColor(np.array(source_image), cv2.COLOR_RGB2BGR)
    
    # Botón para activar el procesamiento
    if st.button("Ejecutar Reconocimiento"):
        with st.spinner("Procesando imagen..."):
            
            # 3 y 4. Segmentación e Interpretación: El modelo predice las clases
            results = model(opencv_image)
            
            # Dibujar los resultados sobre la imagen original
            # plot() de YOLO devuelve la imagen con cajas y etiquetas ya renderizadas
            annotated_frame = results[0].plot()
            
            # Convertir de vuelta a RGB para mostrarlo correctamente en Streamlit
            annotated_image_rgb = cv2.cvtColor(annotated_frame, cv2.COLOR_BGR2RGB)
            
            # Mostrar el resultado final en la interfaz
            st.success("¡Procesamiento Completado!")
            st.image(annotated_image_rgb, caption="Objetos Clasificados y Reconocidos", use_container_width=True)
            
            # Desplegar un desglose de los objetos detectados
            st.subheader("Objetos detectados en la escena:")
            boxes = results[0].boxes
            if len(boxes) == 0:
                st.write("No se detectó ningún objeto conocido.")
            else:
                for box in boxes:
                    cls_id = int(box.cls[0])
                    label = model.names[cls_id]
                    confidence = float(box.conf[0]) * 100
                    st.write(f"• **{label.capitalize()}** con un **{confidence:.2f}%** de confianza.")