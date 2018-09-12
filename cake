#!/usr/bin/env bash
################################################################################
#
# Bake is a shell script for running CakePHP bake script
#
# CakePHP(tm) :  Rapid Development Framework (https://cakephp.org)
# Copyright (c) Cake Software Foundation, Inc. (https://cakefoundation.org)
#
# Licensed under The MIT License
# For full copyright and license information, please see the LICENSE.txt
# Redistributions of files must retain the above copyright notice.
#
# @copyright     Copyright (c) Cake Software Foundation, Inc. (https://cakefoundation.org)
# @link          https://cakephp.org CakePHP(tm) Project
# @package       app.Console
# @since         CakePHP(tm) v 1.2.0.5012
# @license       https://opensource.org/licenses/mit-license.php MIT License
#
################################################################################

# Canonicalize by following every symlink of the given name recursively
canonicalize() {
	NAME="$1"
	if [ -f "$NAME" ]
	then
		DIR=$(dirname -- "$NAME")
		NAME=$(cd -P "$DIR" && pwd -P)/$(basename -- "$NAME")
	fi
	while [ -h "$NAME" ]; do
		DIR=$(dirname -- "$NAME")
		SYM=$(readlink "$NAME")
		NAME=$(cd "$DIR" && cd $(dirname -- "$SYM") && pwd)/$(basename -- "$SYM")
	done
	echo "$NAME"
}

CONSOLE=$(dirname -- "$(canonicalize "$0")")
APP=$(dirname "$CONSOLE")

exec php -q "$CONSOLE"/cake.php -working "$APP" "$@"
exit
