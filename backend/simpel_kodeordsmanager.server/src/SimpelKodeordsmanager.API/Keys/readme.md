# Generate PEM Keys

### Generate a Private Key
openssl genrsa -out PrivateKey.pem

### Generate a Public Key from the Private Key
openssl rsa -in PrivateKey.pem -outform PEM -pubout -out PublicKey.pem

