import cv2
import os

# Crear carpeta de resultados si no existe
os.makedirs("resultados", exist_ok=True)

# 1. Cargar las 3 imágenes
img1 = cv2.imread("imagenes_origen/mrincreible.jpg")
img2 = cv2.imread("imagenes_origen/perrito.jpg")
img3 = cv2.imread("imagenes_origen/roni.jpg")

# 2. Encontrar las dimensiones máximas (alto, ancho)
# img.shape retorna (alto, ancho, canales)
max_alto = max(img1.shape[0], img2.shape[0], img3.shape[0])
max_ancho = max(img1.shape[1], img2.shape[1], img3.shape[1])
dimension_final = (max_ancho, max_alto)  # OpenCV usa (ancho, alto) para resize

# 3. Redimensionar todas a la dimensión más grande
img1_res = cv2.resize(img1, dimension_final, interpolation=cv2.INTER_CUBIC)
img2_res = cv2.resize(img2, dimension_final, interpolation=cv2.INTER_CUBIC)
img3_res = cv2.resize(img3, dimension_final, interpolation=cv2.INTER_CUBIC)

# 4. Guardar las imágenes temporales redimensionadas para el siguiente paso
cv2.imwrite("resultados/img1_res.png", img1_res)
cv2.imwrite("resultados/img2_res.png", img2_res)
cv2.imwrite("resultados/img3_res.png", img3_res)

print(f"Imágenes redimensionadas con éxito a: {dimension_final}")