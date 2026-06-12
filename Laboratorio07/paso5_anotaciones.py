import cv2

# Cargar una de las imágenes originales (asegúrate de que tenga una persona/animal)
img = cv2.imread("imagenes_origen/mrincreible.jpg")

# Definir coordenadas manuales estimadas de la cara (ajusta x, y según imagene de mrincreible)
centro_x, centro_y = 112, 112  
radio = 80

# 1. Dibujar el círculo (Imagen, Centro, Radio, Color BGR, Grosor de línea)
cv2.circle(img, (centro_x, centro_y), radio, (0, 255, 0), 3)

# 2. Añadir texto (Imagen, Texto, Origen_Coordenadas, Fuente, Escala, Color BGR, Grosor)
posicion_texto = (centro_x - radio, centro_y - radio - 15)
cv2.putText(img, "Mr Increible", posicion_texto, 
            cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2, cv2.LINE_AA)

# Guardar resultado
cv2.imwrite("resultados/imagen_anotada.png", img)
cv2.imshow("Anotaciones", img)
cv2.waitKey(0)
cv2.destroyAllWindows()