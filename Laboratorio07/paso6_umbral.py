import cv2

# El umbral binario requiere obligatoriamente una imagen en escala de grises
img_gris = cv2.imread("resultados/imagen_grises.png", cv2.IMREAD_GRAYSCALE)

# Aplicar umbral binario: si el píxel supera 127 se vuelve 255 (blanco), sino 0 (negro)
valor_umbral = 127
max_valor = 255
_, img_threshold = cv2.threshold(img_gris, valor_umbral, max_valor, cv2.THRESH_BINARY)

# Guardar y mostrar
cv2.imwrite("resultados/imagen_umbral.png", img_threshold)
cv2.imshow("Umbral Binario", img_threshold)
cv2.waitKey(0)
cv2.destroyAllWindows()