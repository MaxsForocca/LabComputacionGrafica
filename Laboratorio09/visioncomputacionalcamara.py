import cv2
from ultralytics import YOLO
import cvzone

# 1. Adquisición: Inicializar la cámara web (0 es la cámara integrada)
cap = cv2.VideoCapture(0)
cap.set(3, 1280) # Ancho de la ventana
cap.set(4, 720)  # Alto de la ventana

# Cargar el modelo preentrenado de YOLO (detecta 80 clases comunes)
model = YOLO("yolov8n.pt")
while True:
    success, img = cap.read()
    if not success:
        break

    # 2. Limpieza/Preprocesamiento: En este caso, YOLO se encarga internamente 
    # de redimensionar y normalizar la imagen.
    
    # 3 y 4. Segmentación e Interpretación: El modelo procesa la imagen
    results = model(img, stream=True)
    for r in results:
        boxes = r.boxes
        for box in boxes:
            # Coordenadas de la caja delimitadora (Segmentación)
            x1, y1, x2, y2 = box.xyxy[0]
            x1, y1, x2, y2 = int(x1), int(y1), int(x2), int(y2)
            
            # Confianza de la predicción (en porcentaje)
            conf = round(float(box.conf[0]) * 100, 2)
            
            # Obtener el índice y nombre de la clase reconocida (Interpretación)
            cls = int(box.cls[0])
            name = model.names[cls]

            # Filtrar por un umbral de confianza (ej. mayor al 10%) para evitar falsos positivos
            if conf > 10:
                # Dibujar una caja usando cvzone alrededor del objeto
                cvzone.cornerRect(img, (x1, y1, x2 - x1, y2 - y1), l=15, rt=2, colorR=(255, 0, 0))
                # Mostrar el nombre del objeto y su confianza
                cvzone.putTextRect(img, f'{name} {conf}%', (max(0, x1), max(35, y1)), 
                                   scale=1, thickness=1, colorR=(0, 255, 0))

    # Mostrar el resultado final en una ventana
    cv2.imshow("Clasificación y Reconocimiento - Laboratorio 9", img)
    
    # Cerrar la aplicación si se presiona la tecla 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()