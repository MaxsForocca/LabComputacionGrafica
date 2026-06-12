import cv2

# Cargar las imágenes ya redimensionadas del paso anterior
img1 = cv2.imread("resultados/img1_res.png")
img2 = cv2.imread("resultados/img2_res.png")
img3 = cv2.imread("resultados/img3_res.png")

# Separar los canales B, G, R de cada imagen independiente
# Recuerda: OpenCV extrae en orden B=0, G=1, R=2
_, _, r_img1 = cv2.split(img1)
_, g_img2, _ = cv2.split(img2)
b_img3, _, _ = cv2.split(img3)

# Fusionar siguiendo estrictamente el formato BGR de OpenCV
imagen_combinada = cv2.merge([b_img3, g_img2, r_img1])

# Guardar y mostrar el resultado
cv2.imwrite("resultados/imagen_combinada.png", imagen_combinada)
cv2.imshow("Imagen Combinada", imagen_combinada)
cv2.waitKey(0)
cv2.destroyAllWindows()