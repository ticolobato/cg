import socket
import threading

class ThreadedServer(object):
    def __init__(self, host, port):
        self.host = host
        self.port = port
        self.sock = socket.socket()
        self.sock.bind((self.host, self.port))

    def listen(self):
        self.sock.listen(5)
        while True:
            client, address = self.sock.accept()
            client.settimeout(60)
            threading.Thread(target = self.listenToClient,args = (client,address)).start()

    def listenToClient(self, client, address):
        size = 1024
        while True:
            try:
                data = client.recv(size)
                if data:
                    # Set the response to echo back the recieved data 
                    mensagem_cliente = data.decode().split() # Separando a mensagem do cliente
                    ID_mensagem = mensagem_cliente[1] # pegando o ID da mensagem
                    preco = 999999

                    if mensagem_cliente[0] == 'D':
                        arq = open("valores.txt", "a")
                        for k in range(2, len(mensagem_cliente)):
                            arq.write(mensagem_cliente[k] + " ") # Salvar preço do posto no arquivo de texto
                        # arq.write("\n")
                        arq.close()

                    if mensagem_cliente[0] == 'print':
                        arq =  open("valores.txt", "r")
                        for linha in arq: 
                            print(linha) # Ler preços dos postos do arquivo

                    if mensagem_cliente[0] == 'P':
                        rangeLat = (int(mensagem_cliente[4]) - int(mensagem_cliente[3]), int(mensagem_cliente[4]) + int(mensagem_cliente[3])) # Intervalo da busca
                        rangeLon = (int(mensagem_cliente[5]) - int(mensagem_cliente[3]), int(mensagem_cliente[5]) + int(mensagem_cliente[3])) # Intervalo da busca
                        with open("valores.txt") as arq:
                            for linha in arq: 
                                mensagem_arquivo = linha.split( ) # Lendo os preços do arquivo e comparando para pegar o menor
                                if (int(mensagem_arquivo[2]) >= rangeLat[0] and int(mensagem_arquivo[2]) <= rangeLat[1] and
                                    int(mensagem_arquivo[3]) >= rangeLon[0] and int(mensagem_arquivo[3]) <= rangeLon[1] and 
                                    mensagem_arquivo[0] == mensagem_cliente[2]): # Garantir o intervalo da busca e o tipo de combustivel
                                        if int(mensagem_arquivo[1]) < preco:
                                            preco = int(mensagem_arquivo[1]) # Pegar menor preço
                        if preco != 999999:
                            client.send(str.encode("O menor preço encontrado foi: " + str(preco)))
                            preco = 999999
							
                    
                else:
                    raise error('Client disconnected')
            except:
                client.close()
                return False

if __name__ == "__main__":
    while True:
        port_num = input("Port? ")
        try:
            port_num = int(port_num)
            break
        except ValueError:
            pass

    ThreadedServer('',port_num).listen()