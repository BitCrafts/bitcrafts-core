.PHONY: build clean restore test

# Variables
CONFIGURATION = Debug
TARGET_FRAMEWORK = net9.0
OUTPUT_DIR = bin/$(CONFIGURATION)/$(TARGET_FRAMEWORK)
DEMO_MODULES_DIR = chemin/vers/votre/demo/app


all: restore build test demo

restore:
	dotnet restore src/BitCrafts.Core/BitCrafts.Core.sln

build:
	dotnet build src/BitCrafts.Core/BitCrafts.Core.sln --configuration $(CONFIGURATION) --framework $(TARGET_FRAMEWORK)

test:
	dotnet test src/BitCrafts.Core/BitCrafts.Core.sln --configuration $(CONFIGURATION) --framework $(TARGET_FRAMEWORK)

clean:
	dotnet clean src/BitCrafts.Core/BitCrafts.Core.sln

demo:
	cp src/BitCrafts.Core/BitCrafts.Modules.Users/bin/$(CONFIGURATION)/$(TARGET_FRAMEWORK)/BitCrafts.Modules.Users.dll samples/BitCrafts.ConsoleDemo/bin/$(CONFIGURATION)/$(TARGET_FRAMEWORK)/Modules

