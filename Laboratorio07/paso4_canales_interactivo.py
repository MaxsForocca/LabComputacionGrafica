import cv2
import numpy as np

img = cv2.imread("resultados/imagen_combinada.png")
b_org, g_org, r_org = cv2.split(img)

# Variables de estado booleanas para controlar la visibilidad
ver_r, ver_g, ver_b = True, True, True

print("Controles:\n'r' -> Alternar Rojo\n'g' -> Alternar Verde\n'b' -> Alternar Azul\n'ESC' -> Salir")

while True:
    # Si el canal está desactivado, creamos una matriz vacía (ceros) del mismo tamaño
    r = r_org if ver_r else np.zeros_like(r_org)
    g = g_org if ver_g else np.zeros_like(g_org)
    b = b_org if ver_b else np.zeros_like(b_org)
    
    # Re-acoplar la imagen dinámica
    img_dinamica = cv2.merge([b, g, r])
    cv2.imshow("Visor Interactivo de Canales", img_dinamica)
    
    # Capturar la tecla presionada (espera 10ms)
    tecla = cv2.waitKey(10) & 0xFF
    
    if tecla == ord('r') or tecla == ord('R'):
        ver_r = not ver_r
    elif tecla == ord('g') or tecla == ord('G'):
        ver_g = not ver_g
    elif tecla == ord('b') or tecla == ord('B'):
        ver_b = not ver_b
    elif tecla == 27:  # Código ASCII de la tecla Escape
        break

cv2.destroyAllWindows()