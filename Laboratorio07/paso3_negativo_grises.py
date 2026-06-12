import cv2

# 1. Cargar la imagen combinada
img_comb = cv2.imread("resultados/imagen_combinada.png")

# 2. Calcular el negativo (invertir bits: 255 - valor_pixel)
img_negativo = cv2.bitwise_not(img_comb)
cv2.imwrite("resultados/imagen_negativa.png", img_negativo)

# 3. Cargar la imagen en escala de grises directamente desde el archivo guardado
# El parámetro cv2.IMREAD_GRAYSCALE (o el número 0) hace la conversión al leer
img_grises = cv2.imread("resultados/imagen_negativa.png", cv2.IMREAD_GRAYSCALE)
cv2.imwrite("resultados/imagen_grises.png", img_grises)

# Mostrar ambos resultados
cv2.imshow("Negativo", img_negativo)
cv2.imshow("Escala de Grises", img_grises)
cv2.waitKey(0)
cv2.destroyAllWindows()